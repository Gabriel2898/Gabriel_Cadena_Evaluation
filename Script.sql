
CREATE DATABASE ProductDB;
GO

USE ProductDB;
GO


CREATE TABLE Products (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL,
    Description NVARCHAR(500),
    Category NVARCHAR(50) NOT NULL,
    ImageUrl NVARCHAR(255),
    Price DECIMAL(18,2) NOT NULL,
    Stock INT NOT NULL DEFAULT 0,
    CreatedAt DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME(),
    UpdatedAt DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME(),
    CONSTRAINT CHK_Price CHECK (Price > 0),
    CONSTRAINT CHK_Stock CHECK (Stock >= 0)
);
GO

CREATE INDEX IX_Products_Category ON Products(Category);
CREATE INDEX IX_Products_Name ON Products(Name);
GO

INSERT INTO Products (Name, Description, Category, Price, Stock)
VALUES 
('Laptop HP EliteBook', 'Laptop empresarial de 14 pulgadas', 'Electrónicos', 1200.99, 15),
('iPhone 13', 'Teléfono inteligente de 128GB', 'Electrónicos', 899.00, 30),
('Silla ergonómica', 'Silla de oficina ajustable', 'Muebles', 249.50, 8),
('Monitor 24"', 'Monitor Full HD IPS', 'Electrónicos', 179.99, 12),
('Teclado mecánico', 'Teclado RGB switches azules', 'Accesorios', 89.99, 25);
GO

CREATE PROCEDURE sp_UpdateProductStock
    @ProductId INT,
    @Quantity INT
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;
        
        UPDATE Products 
        SET Stock = Stock + @Quantity,
            UpdatedAt = SYSUTCDATETIME()
        WHERE Id = @ProductId;
        
        IF @@ROWCOUNT = 0
            RAISERROR('Producto no encontrado', 16, 1);
            
        COMMIT TRANSACTION;
        RETURN 0; 
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;
            
        RETURN -1; 
    END CATCH
END;
GO






CREATE DATABASE TransactionDB;
GO

USE TransactionDB;
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Transactions')
    DROP TABLE Transactions;
GO

CREATE TABLE Transactions (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    TransactionDate DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME(),
    TransactionType NVARCHAR(10) NOT NULL CHECK (TransactionType IN ('Purchase', 'Sale')),
    ProductId INT NOT NULL,
    Quantity INT NOT NULL CHECK (Quantity > 0),
    UnitPrice DECIMAL(18,2) NOT NULL CHECK (UnitPrice > 0),
    TotalPrice DECIMAL(18,2) NOT NULL CHECK (TotalPrice > 0),
    Details NVARCHAR(500),
    CONSTRAINT CHK_TotalPrice CHECK (TotalPrice = Quantity * UnitPrice)
);
GO

CREATE INDEX IX_Transactions_ProductId ON Transactions(ProductId);
CREATE INDEX IX_Transactions_TransactionDate ON Transactions(TransactionDate);
CREATE INDEX IX_Transactions_TransactionType ON Transactions(TransactionType);
GO

INSERT INTO Transactions (TransactionType, ProductId, Quantity, UnitPrice, TotalPrice, Details)
VALUES 
('Purchase', 1, 10, 1150.00, 11500.00, 'Compra inicial de inventario'),
('Purchase', 2, 20, 850.00, 17000.00, 'Compra mayorista'),
('Sale', 1, 2, 1200.99, 2401.98, 'Venta a cliente regular'),
('Purchase', 3, 5, 150.00, 750.00, 'Reposición de stock');
GO

CREATE OR ALTER VIEW vw_TransactionDetails AS
SELECT 
    t.Id,
    t.TransactionDate,
    t.TransactionType,
    t.ProductId,
    t.Quantity,
    t.UnitPrice,
    t.TotalPrice,
    t.Details
FROM Transactions t;
GO

CREATE OR ALTER PROCEDURE sp_RegisterTransaction
    @TransactionType NVARCHAR(10),
    @ProductId INT,
    @Quantity INT,
    @UnitPrice DECIMAL(18,2),
    @Details NVARCHAR(500) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    
    BEGIN TRY
        BEGIN TRANSACTION;
        
        IF @TransactionType NOT IN ('Purchase', 'Sale')
            THROW 51000, 'Tipo de transacción no válido. Debe ser "Purchase" o "Sale"', 1;
            
        IF @Quantity <= 0
            THROW 51000, 'La cantidad debe ser mayor que cero', 1;
            
        IF @UnitPrice <= 0
            THROW 51000, 'El precio unitario debe ser mayor que cero', 1;
        
        INSERT INTO Transactions (
            TransactionType,
            ProductId,
            Quantity,
            UnitPrice,
            TotalPrice,
            Details
        )
        VALUES (
            @TransactionType,
            @ProductId,
            @Quantity,
            @UnitPrice,
            @Quantity * @UnitPrice,
            @Details
        );
        
        COMMIT TRANSACTION;
        RETURN 0; 
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;
            
        THROW;
    END CATCH
END;
GO

