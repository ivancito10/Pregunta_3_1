DROP DATABASE BDIvan;

CREATE DATABASE BDIvan

USE BDIvan

CREATE TABLE Persona (
    PersonaID INT PRIMARY KEY IDENTITY,
    Nombre NVARCHAR(100),
    Apellido NVARCHAR(100),
    FechaNacimiento DATE,
    Direccion NVARCHAR(200)
);

CREATE TABLE CuentaBancaria (
    CuentaID INT PRIMARY KEY IDENTITY,
    PersonaID INT FOREIGN KEY REFERENCES Persona(PersonaID),
    TipoCuenta NVARCHAR(50),
    Saldo DECIMAL(18, 2),
    FechaApertura DATETIME DEFAULT GETDATE() -- Utiliza GETDATE() como valor predeterminado para obtener la fecha y hora actuales
);

CREATE TABLE TransaccionesBancarias (
    TransaccionID INT PRIMARY KEY IDENTITY,
    CuentaID INT FOREIGN KEY REFERENCES CuentaBancaria(CuentaID),
    TipoTransaccion NVARCHAR(50),
    Monto DECIMAL(18, 2),
    FechaTransaccion DATETIME DEFAULT GETDATE(), -- Utiliza GETDATE() como valor predeterminado para obtener la fecha y hora actuales
    CONSTRAINT CHK_Monto_Positive CHECK (Monto >= 0) -- Para asegurar que el monto sea positivo
);


-- Insertar datos en la tabla Persona (no incluir la columna PersonaID)
INSERT INTO Persona (Nombre, Apellido, FechaNacimiento, Direccion) 
VALUES 
('Juan', 'Perez', '1990-05-15', 'Calle Principal 123'),
('Maria', 'Gonzalez', '1985-08-20', 'Avenida Central 456'),
('Pedro', 'Martinez', '1978-12-10', 'Plaza Mayor 789');

-- Insertar datos en la tabla CuentaBancaria (no incluir la columna CuentaID)
INSERT INTO CuentaBancaria (PersonaID, TipoCuenta, Saldo)
VALUES 
(1, 'Ahorros', 5000.00),
(1, 'Corriente', 10000.00),
(2, 'Ahorros', 400.00),
(2, 'Corriente', 2000.00),
(3, 'Ahorros', 3000.00),
(3, 'Corriente', 11000.00);

-- Insertar datos en la tabla TransaccionesBancarias (no incluir la columna TransaccionID)
INSERT INTO TransaccionesBancarias (CuentaID, TipoTransaccion, Monto)
VALUES 
(2, 'Deposito', 1000.00),
(3, 'Retiro', 300.00);

CREATE PROCEDURE RegistrarCuentaBancaria
(
    @PersonaID INT,
    @TipoCuenta NVARCHAR(50),
    @Saldo DECIMAL(18, 2)
)
AS
BEGIN
    INSERT INTO CuentaBancaria (PersonaID, TipoCuenta, Saldo)
    VALUES (@PersonaID, @TipoCuenta, @Saldo);
END

CREATE PROCEDURE EditarCuentaBancaria
(
    @CuentaID INT,
    @TipoCuenta NVARCHAR(50),
    @Saldo DECIMAL(18, 2)
)
AS
BEGIN
    UPDATE CuentaBancaria
    SET TipoCuenta = @TipoCuenta, Saldo = @Saldo
    WHERE CuentaID = @CuentaID;
END



CREATE PROCEDURE EliminarCuentaBancaria
(
    @CuentaID INT
)
AS
BEGIN
    DELETE FROM CuentaBancaria
    WHERE CuentaID = @CuentaID;
END


select * from Persona;
select * from CuentaBancaria;
select * from TransaccionesBancarias;