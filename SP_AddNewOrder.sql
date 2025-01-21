CREATE PROCEDURE AddNewOrder
    @Empid INT,
	@Custid INT,
    @Shipperid INT,
    @Shipname NVARCHAR(100),
    @Shipaddress NVARCHAR(200),
    @Shipcity NVARCHAR(100),
    @Orderdate DATE,
    @Requireddate DATE,
    @Shippeddate DATE,
    @Freight DECIMAL(10, 2),
    @Shipcountry NVARCHAR(100),
    @Productid INT,        
    @Unitprice DECIMAL(10, 2), 
    @Qty INT,              
    @Discount DECIMAL(5, 2)   
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        INSERT INTO [StoreSample].[Sales].[Orders] (
            Custid,Empid, Shipperid, Shipname, Shipaddress, Shipcity, 
            Orderdate, Requireddate, Shippeddate, Freight, Shipcountry
        )
        VALUES (
            @Custid, @Empid, @Shipperid, @Shipname, @Shipaddress, @Shipcity, 
            @Orderdate, @Requireddate, @Shippeddate, @Freight, @Shipcountry
        );

        DECLARE @OrderId INT = SCOPE_IDENTITY();

        INSERT INTO [StoreSample].[Sales].[OrderDetails] (
            Orderid, Productid, Unitprice, Qty, Discount
        )
        VALUES (
            @OrderId, @Productid, @Unitprice, @Qty, @Discount
        );

        SELECT 'Order created successfully!' AS Message, @OrderId AS OrderId;

    END TRY
    BEGIN CATCH
        DECLARE @ErrorMessage NVARCHAR(MAX) = ERROR_MESSAGE();
        RAISERROR(@ErrorMessage, 16, 1);
    END CATCH;
END;
GO
