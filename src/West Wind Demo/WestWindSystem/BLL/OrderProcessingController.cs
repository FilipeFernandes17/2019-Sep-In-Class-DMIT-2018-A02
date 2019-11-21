using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WestWindSystem.DAL;
using WestWindSystem.DataModels;
using WestWindSystem.Entities;

namespace WestWindSystem.BLL
{
    [DataObject]
    public class OrderProcessingController
    {
        #region Queries
        [DataObjectMethod(DataObjectMethodType.Select)]
        public List<OutstandingOrder> LoadOrders(int supplierId)
        {
            using (var context = new WestWindContext())
            {
                // Validation:
                //      - Make sure the supplier ID exists, otherwise throw exception
                var supplier = context.Suppliers.Find(supplierId);
                if (supplier == null)
                    throw new Exception("Unknown supplier");
                //      - [Advanced:] Make sure the logged-in user works for the identified supplier.
                // Processing
                var result = from ord in context.Orders
                             where !ord.Shipped // Still items to be shipped
                                && ord.OrderDate.HasValue // The order has been placed and is ready to ship
                             select new OutstandingOrder
                             {
                                 OrderId = ord.OrderID,
                                 ShipToName = ord.ShipName,
                                 OrderDate = ord.OrderDate.Value,
                                 RequiredBy = ord.RequiredDate.Value,
                                 OutstandingItems = from detail in ord.OrderDetails
                                                    where detail.Product.SupplierID == supplierId
                                                    select new OrderProductInformation
                                                    {
                                                        ProductId = detail.ProductID,
                                                        ProductName = detail.Product.ProductName,
                                                        Qty = detail.Quantity,
                                                        QtyPerUnit = detail.Product.QuantityPerUnit,
                                                        // TODO:
                                                        //                               Outstanding = (from ship in detail.Order.Shipments
                                                        //                                             from item in ship.ManifestItems
                                                        //                                             where item.ProductID == detail.ProductID
                                                        //                                             select item.ShipQuantity)
                                                    },
                                 //        ord.ShipAddressID,
                                 // Note to self: If there is a ShipTo address, use that, otherwise use the customer address
                                 FullShippingAddress = ord.ShipAddressID.HasValue
                                                     ? context.Addresses.Where(x => x.AddressID == ord.ShipAddressID)
                                                       .Select(a => a.Street + Environment.NewLine +
                                                                    a.City + Environment.NewLine +
                                                                    a.Region + " " +
                                                                    a.Country + ", " +
                                                                    a.PostalCode).FirstOrDefault()
                                                     : ord.Customer.Address.Street + Environment.NewLine +
                                                       ord.Customer.Address.City + Environment.NewLine +
                                                       ord.Customer.Address.Region + " " +
                                                       ord.Customer.Address.Country + ", " +
                                                       ord.Customer.Address.PostalCode,
                                 Comments = ord.Comments
                             };
                return result.ToList();
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public List<ShipperSelection> ListShippers()
        {
            using(var context = new WestWindContext())
            {
                var result = from shipper in context.Shippers
                             orderby shipper.CompanyName
                             select new ShipperSelection
                             {
                                 ShipperId = shipper.ShipperID,
                                 Shipper = shipper.CompanyName
                             };
                return result.ToList();
            }
        }
        #endregion

        #region Commands
        public void ShipOrder(int orderId, ShippingDirections shipping, List<ProductShipment> products)
        {
            using (var context = new WestWindContext())
            {
                // TODO: Validation:
                var existingOrder = context.Orders.Find(orderId);
                //A) OrderId must be valid
                if (existingOrder == null)
                    throw new Exception("Order does not exist");
                if (existingOrder.Shipped)
                    throw new Exception("This Order has already been completed");
                if (!existingOrder.OrderDate.HasValue)
                    throw new Exception("This order is not ready to be shipped (no order data has been specified)");
                //B) products cannot be an empty list
                if (existingOrder == null || !products.Any())
                    throw new Exception("No Products identified for shipping");
                //C) Products identified must be on the order
                foreach (var item in products)
                {
                    if (item == null) throw new Exception("Blank item listed in products to be shipped");
                    if (!existingOrder.OrderDetails.Any(x => x.ProductID == item.ProductId))
                        throw new Exception($"The product {item.ProductId} does not exist on the order");
                    //TODO: D) Quantity must be greater than zero and less than or equal to the quantity outstanding
                }
                if (shipping == null)
                    throw new Exception("No shipping details provided");
                var shipper = context.Shippers.Find(shipping.ShipperId);
                if (shipper == null) throw new Exception("Invalid shipper id");
                //E) Shipper must exist
                if (shipping.FreightCharge.HasValue && shipping.FreightCharge <= 0)
                    throw new Exception("tive value or no charge.");

                /*Validation:
                    OrderId must be valid
                    products cannot be an empty list
                    Products identified must be on the order
                    Quantity must be greater than zero and less than or equal to the quantity outstanding
                    Shipper must exist
                    Freight charge must either be null (no charge) or > $0.00
                */
                // TODO: Processing the shipment
                var ship = new Shipment
                {
                    OrderID = orderId,
                    ShipVia = shipping.ShipperId,
                    TrackingCode = shipping.TrackingCode,
                    FreightCharge = shipping.FreightCharge.HasValue
                                  ? shipping.FreightCharge.Value
                                  : 0,
                    ShippedDate = DateTime.Now
                };
                //2) add all manifest items
                foreach(var item in products)
                {
                    ship.ManifestItems.Add(new ManifestItem
                    {
                        ProductID = item.ProductId,
                        ShipQuantity = (short)item.ShipQuantity //TODO: change the datatype on ProductShipment.ShipQuantity
                    });
                }
                //TODO: 3) Check if order is complete; if so, update order.Shipped

                //4) Add Shipment to the database
                context.Shipments.Add(ship);

                //5) Save the changes as a single transaction
                context.SaveChanges();
                /*Processing (tables/data that must be updated/inserted/deleted/whatever)
                    Create new Shipment
                    Add all manifest items
                    Check if order is complete; if so, update Order.Shipped
                 */
            }
            throw new NotImplementedException();
           
            
        }
        #endregion
    }
}
