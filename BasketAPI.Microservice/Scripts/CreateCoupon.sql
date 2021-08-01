/* pgAdmin (Add New Server -> Name: Local -> Host name/address: coupon.grpc.db -> Password: admin1234) 
            Servers -> Local -> Databases -> CouponDB -> Schemas -> public -> Tables -> *Right button click* -> Query Tool) */
CREATE TABLE Coupon(Id SERIAL PRIMARY KEY,
	ProductName VARCHAR(24) NOT NULL,
	Description TEXT,
	Amount DECIMAL);
	
INSERT INTO Coupon (ProductName, Description, Amount) VALUES ('Product 01', 'COUPON FOR PRODUCT01', 5.50);
INSERT INTO Coupon (ProductName, Description, Amount) VALUES ('Product 02', 'COUPON FOR PRODUCT02', 6.50);
INSERT INTO Coupon (ProductName, Description, Amount) VALUES ('Product 03', 'COUPON FOR PRODUCT03', 7.50);
INSERT INTO Coupon (ProductName, Description, Amount) VALUES ('Product 04', 'COUPON FOR PRODUCT04', 8.50);
INSERT INTO Coupon (ProductName, Description, Amount) VALUES ('Product 05', 'COUPON FOR PRODUCT05', 9.50);

/*
    POST http://dockerhost:2000/swagger
    {
      "userName": "User01",
      "items": [
        {
          "productId": "PRODUCT01",
          "productName": "Product 01",
          "quantity": 2,
          "price": 34.5
        },
        {
          "productId": "PRODUCT02",
          "productName": "Product 02",
          "quantity": 1,
          "price": 14.2
        }
      ]
    }
*/
