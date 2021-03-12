using System;
using System.Data;
using System.Data.SqlClient;

namespace AutoLevelHomework
{
    class Program
    {
        static void Main(string[] args)
        {
            DataSet shopDb = new DataSet("ShopDb");
            DataTable orders = new DataTable("Orders");
            shopDb.Tables.Add(orders);


            DataColumn orderIdColumn = new DataColumn("Id", Type.GetType("System.Int32"));
            orderIdColumn.Unique = true; 
            orderIdColumn.AllowDBNull = false; 
            orderIdColumn.AutoIncrement = true; 
            orderIdColumn.AutoIncrementSeed = 1; 
            orderIdColumn.AutoIncrementStep = 1; 


            DataColumn orderDetailsId = new DataColumn("orderDetailsId", Type.GetType("System.Int32"));
            DataColumn sum = new DataColumn("Sum", Type.GetType("System.Double"));
           
            orders.Columns.Add(orderIdColumn);
            orders.Columns.Add(orderDetailsId);
            orders.Columns.Add(sum);

            orders.PrimaryKey = new DataColumn[] { orders.Columns["Id"] };



            DataTable customers = new DataTable("Customers");
            shopDb.Tables.Add(customers);


            DataColumn customersIdColumn = new DataColumn("Id", Type.GetType("System.Int32"));
            customersIdColumn.Unique = true;
            customersIdColumn.AllowDBNull = false;
            customersIdColumn.AutoIncrement = true;
            customersIdColumn.AutoIncrementSeed = 1;
            customersIdColumn.AutoIncrementStep = 1;

            customers.Columns.Add(customersIdColumn);
            customers.PrimaryKey = new DataColumn[] { customers.Columns["Id"] };
            customers.Columns.Add("FullName", Type.GetType("System.String"));




            DataTable employees = new DataTable("Emloyees");
            shopDb.Tables.Add(employees);

            DataColumn employeesIdColumn = new DataColumn("Id", Type.GetType("System.Int32"));
            employeesIdColumn.Unique = true;
            employeesIdColumn.AllowDBNull = false;
            employeesIdColumn.AutoIncrement = true;
            employeesIdColumn.AutoIncrementSeed = 1;
            employeesIdColumn.AutoIncrementStep = 1;

            employees.Columns.Add(employeesIdColumn);

            employees.PrimaryKey = new DataColumn[] { employees.Columns["Id"] };

            employees.Columns.Add("FullName", Type.GetType("System.String"));
            employees.Columns.Add("Salary", Type.GetType("System.Double"));
            employees.Columns.Add("Position", Type.GetType("System.String"));

            

            DataTable orderDetails = new DataTable("OrderDetails");
            shopDb.Tables.Add(orderDetails);

            DataColumn orderDetailsIdColumn = new DataColumn("Id", Type.GetType("System.Int32"));
            orderDetailsIdColumn.Unique = true;
            orderDetailsIdColumn.AllowDBNull = false;
            orderDetailsIdColumn.AutoIncrement = true;
            orderDetailsIdColumn.AutoIncrementSeed = 1;
            orderDetailsIdColumn.AutoIncrementStep = 1;

            orderDetails.Columns.Add(orderDetailsIdColumn);
            orderDetails.PrimaryKey = new DataColumn[] { orderDetails.Columns["Id"] };
            orderDetails.Columns.Add("CustomerId", Type.GetType("System.Int32"));
            orderDetails.Columns.Add("ProductId", Type.GetType("System.Int32"));
            orderDetails.Columns.Add("EmployeeId", Type.GetType("System.Int32"));



            DataTable products = new DataTable("Products");
            shopDb.Tables.Add(products);

            DataColumn productIdColumn = new DataColumn("Id", Type.GetType("System.Int32"));
            productIdColumn.Unique = true;
            productIdColumn.AllowDBNull = false;
            productIdColumn.AutoIncrement = true;
            productIdColumn.AutoIncrementSeed = 1;
            productIdColumn.AutoIncrementStep = 1;

            products.Columns.Add(productIdColumn);
            products.PrimaryKey = new DataColumn[] { products.Columns["Id"] };
            products.Columns.Add("Name", Type.GetType("System.String"));
            products.Columns.Add("Price", Type.GetType("System.Double"));


            ForeignKeyConstraint foreignKeyOrders = new ForeignKeyConstraint(orderDetails.Columns["Id"], orders.Columns["orderDetailsId"])
            {
                ConstraintName = "orders_OrderDetails_fk",
                DeleteRule = Rule.SetNull,
                UpdateRule = Rule.Cascade
            };
            shopDb.Tables["orders"].Constraints.Add(foreignKeyOrders);
            shopDb.EnforceConstraints = true;
            shopDb.Relations.Add("OrdersDetailsRel", orderDetails.Columns["Id"], orders.Columns["orderDetailsId"]);



            ForeignKeyConstraint orderCustomerFK = new ForeignKeyConstraint(customers.Columns["Id"], orderDetails.Columns["CustomerId"])
            {
                ConstraintName = "orders_customers_fk",
                DeleteRule = Rule.SetNull,
                UpdateRule = Rule.Cascade
            };
            shopDb.Tables["OrderDetails"].Constraints.Add(orderCustomerFK);
            shopDb.EnforceConstraints = true;
            shopDb.Relations.Add("OrderCustomerRel", customers.Columns["Id"], orderDetails.Columns["CustomerId"]);



            ForeignKeyConstraint orderProductFK = new ForeignKeyConstraint(products.Columns["Id"], orderDetails.Columns["ProductId"])
            {
                ConstraintName = "orders_product_fk",
                DeleteRule = Rule.SetNull,
                UpdateRule = Rule.Cascade
            };
            shopDb.Tables["OrderDetails"].Constraints.Add(orderProductFK);
            shopDb.EnforceConstraints = true;
            shopDb.Relations.Add("OrderProductRel", products.Columns["Id"], orderDetails.Columns["ProductId"]);



            ForeignKeyConstraint orderEmployeeFK = new ForeignKeyConstraint(employees.Columns["Id"], orderDetails.Columns["EmployeeId"])
            {
                ConstraintName = "orders_employee_fk",
                DeleteRule = Rule.SetNull,
                UpdateRule = Rule.Cascade
            };
            shopDb.Tables["OrderDetails"].Constraints.Add(orderEmployeeFK);
            shopDb.EnforceConstraints = true;
            shopDb.Relations.Add("OrderEmployeeRel", employees.Columns["Id"], orderDetails.Columns["EmployeeId"]);

        }
    }
}
