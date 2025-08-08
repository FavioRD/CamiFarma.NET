CREATE DATABASE FarmaciaDB;
GO

USE FarmaciaDB;
GO

CREATE TABLE Productos (
    Id INT PRIMARY KEY IDENTITY,
    Nombre NVARCHAR(100),
    Descripcion NVARCHAR(250),
    Precio DECIMAL(10,2),
    Stock INT,
    FechaExpiracion DATE
);


-- Insertar --
CREATE PROCEDURE USP_InsertarProducto
    @Nombre NVARCHAR(100),
    @Descripcion NVARCHAR(250),
    @Precio DECIMAL(10,2),
    @Stock INT,
    @FechaExpiracion DATE
AS
BEGIN
    INSERT INTO Productos (Nombre, Descripcion, Precio, Stock, FechaExpiracion)
    VALUES (@Nombre, @Descripcion, @Precio, @Stock, @FechaExpiracion);
END


-- Listar --
CREATE PROCEDURE USP_ListarProductos
AS
BEGIN
    SELECT * FROM Productos;
END

--
CREATE PROCEDURE USP_ObtenerProductoPorId
    @Id INT
AS
BEGIN
    SELECT * FROM Productos WHERE Id = @Id;
END


-- Actualizar --
CREATE PROCEDURE USP_ActualizarProducto
    @Id INT,
    @Nombre NVARCHAR(100),
    @Descripcion NVARCHAR(250),
    @Precio DECIMAL(10,2),
    @Stock INT,
    @FechaExpiracion DATE
AS
BEGIN
    UPDATE Productos
    SET Nombre = @Nombre,
        Descripcion = @Descripcion,
        Precio = @Precio,
        Stock = @Stock,
        FechaExpiracion = @FechaExpiracion
    WHERE Id = @Id;
END


-- Eliminar --
CREATE PROCEDURE USP_EliminarProducto
    @Id INT
AS
BEGIN
    DELETE FROM Productos WHERE Id = @Id;
END

---- datos ----
EXEC USP_InsertarProducto 'Paracetamol 500mg', 'Analgésico y antipirético', 2.50, 150, '2026-01-15';
EXEC USP_InsertarProducto 'Ibuprofeno 400mg', 'Antiinflamatorio no esteroideo', 3.00, 200, '2026-03-10';
EXEC USP_InsertarProducto 'Amoxicilina 500mg', 'Antibiótico de amplio espectro', 4.50, 120, '2025-12-01';
EXEC USP_InsertarProducto 'Omeprazol 20mg', 'Inhibidor de la bomba de protones', 2.80, 180, '2026-05-30';
EXEC USP_InsertarProducto 'Loratadina 10mg', 'Antihistamínico para alergias', 1.90, 300, '2026-02-28';
EXEC USP_InsertarProducto 'Salbutamol Inhalador', 'Broncodilatador para asma', 5.75, 80, '2025-11-15';
EXEC USP_InsertarProducto 'Metformina 850mg', 'Antidiabético oral', 3.20, 90, '2026-04-10';
EXEC USP_InsertarProducto 'Ácido Fólico 5mg', 'Suplemento vitamínico', 1.25, 250, '2027-01-01';
EXEC USP_InsertarProducto 'Diclofenaco 50mg', 'Analgésico y antiinflamatorio', 2.40, 160, '2025-10-20';
EXEC USP_InsertarProducto 'Ranitidina 150mg', 'Reductor de ácido estomacal', 2.10, 70, '2025-09-01';

