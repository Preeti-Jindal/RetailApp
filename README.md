# RetailApp

#Prerequisites

1) Install My Sql Version 8.0.29

2) Run the following Db Scripts

CREATE DATABASE `retail`;

CREATE TABLE `retail`.`product` ( productId char(36) NOT NULL, productName varchar(45) DEFAULT NULL, price decimal(10,0) DEFAULT NULL, Quantity int DEFAULT NULL, PRIMARY KEY (productId));

CREATE TABLE `retail`.`order_status` ( orderStatusId int NOT NULL AUTO_INCREMENT, orderStatusName varchar(45) DEFAULT NULL, PRIMARY KEY (orderStatusId));

CREATE TABLE `retail`.`order` ( orderId varchar(45) NOT NULL, orderStatus int NOT NULL, orderDate datetime NOT NULL DEFAULT CURRENT_TIMESTAMP, addressLine1 varchar(45) DEFAULT NULL, addressLine2 varchar(45) DEFAULT NULL, city varchar(20) DEFAULT NULL, county varchar(20) DEFAULT NULL, country varchar(20) DEFAULT NULL, eirCode varchar(45) DEFAULT NULL, updatedDate datetime DEFAULT CURRENT_TIMESTAMP, PRIMARY KEY (orderId), KEY OrderStatusKey_idx (orderStatus), CONSTRAINT OrderStatusKey FOREIGN KEY (orderStatus) REFERENCES order_status (orderStatusId));

CREATE TABLE `retail`.`order_item` (orderItemId int NOT NULL AUTO_INCREMENT, orderId varchar(45) DEFAULT NULL, productId char(36) DEFAULT NULL,quantity int DEFAULT NULL, price decimal(10,0) DEFAULT NULL, createdDate datetime DEFAULT CURRENT_TIMESTAMP,updateDate datetime DEFAULT CURRENT_TIMESTAMP, PRIMARY KEY (orderItemId), KEY orderIdKey_idx (orderId), KEY ProductIdKey_idx (productId), CONSTRAINT OrderIdForeignKey FOREIGN KEY (orderId) REFERENCES `order` (orderId), CONSTRAINT ProductIdFoerignKey FOREIGN KEY (productId) REFERENCES `product` (productId));

3) Execute the following scripts to insert some products into Product Table
  
INSERT INTO `retail`.`product`(`productId`,`productName`,`price`,`Quantity`)VALUES(uuid(), 'Shirt', 20,100);

INSERT INTO `retail`.`product`(`productId`,`productName`,`price`,`Quantity`)VALUES(uuid(), 'Socks', 10,200);

INSERT INTO `retail`.`product`(`productId`,`productName`,`price`,`Quantity`)VALUES(uuid(), 'Trouser', 30,1000);

INSERT INTO `retail`.`product`(`productId`,`productName`,`price`,`Quantity`)VALUES(uuid(), 'Diary Of a Wimpy Kid', 5,2000);

INSERT INTO `retail`.`product`(`productId`,`productName`,`price`,`Quantity`)VALUES(uuid(), 'Beast Quest', 5,300);

INSERT INTO `retail`.`product`(`productId`,`productName`,`price`,`Quantity`)VALUES(uuid(), 'Pencils', 5,400);

INSERT INTO `retail`.`product`(`productId`,`productName`,`price`,`Quantity`)VALUES(uuid(), 'Bag', 20,234);

INSERT INTO `retail`.`product`(`productId`,`productName`,`price`,`Quantity`)VALUES(uuid(), 'Pen', 5,345);

INSERT INTO `retail`.`product`(`productId`,`productName`,`price`,`Quantity`)VALUES(uuid(), 'Lego', 100,300);

INSERT INTO `retail`.`product`(`productId`,`productName`,`price`,`Quantity`)VALUES(uuid(), 'Lunch Box', 20,100);

INSERT INTO `retail`.`product`(`productId`,`productName`,`price`,`Quantity`)VALUES(uuid(), 'Shoes', 70,1000);

4) Execute the following scripts to insert order statuses into Order Status table
 
INSERT INTO `retail`.`order_status` (`orderStatusName`) VALUES ('Order Placed');

INSERT INTO `retail`.`order_status` (`orderStatusName`) VALUES ('Pending');

INSERT INTO `retail`.`order_status` (`orderStatusName`) VALUES ('Delivered');

INSERT INTO `retail`.`order_status` (`orderStatusName`) VALUES ('Cancelled');


######################################################################################################

5) Download the project and Open RetailApp soltuion.

6) Run the application and it will display the swagger URL for verification of API's.

######################################################################################################

Notes:
1) Connection string in the project by default set is: Server=127.0.0.1;Database=retail;Uid=root;Pwd=root;port=3306;

2) Scaffolded the existing Retail DB into project using Pomelo.EntityFrameworkCore.MySql library.

3) APIs are defined under OrdersController.

4) The application is integerated with Swagger to list down the APIs and to test get and post API's using Swashbuckle.AspNetCore and Swashbuckle.Core libraries.
 
 
 
 
 
 
 






