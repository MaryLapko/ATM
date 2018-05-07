IF (EXISTS (SELECT * 
                FROM INFORMATION_SCHEMA.TABLES 
                WHERE TABLE_NAME = 'flower_bucket_flower'))
DROP TABLE flower_bucket_flower


IF (EXISTS (SELECT * 
                FROM INFORMATION_SCHEMA.TABLES 
                WHERE TABLE_NAME = 'flower_bucket'))
DROP TABLE flower_bucket


IF (EXISTS (SELECT * 
                FROM INFORMATION_SCHEMA.TABLES 
                WHERE TABLE_NAME = 'flower'))
DROP TABLE flower


CREATE TABLE flower (
	id INT IDENTITY(1,1) PRIMARY KEY,
	name VARCHAR(255) NOT NULL,
	price INT,
	currency VARCHAR(32),
	colour VARCHAR(32)
)

CREATE TABLE flower_bucket (
	id INT IDENTITY(1,1) PRIMARY KEY,
	price int,
	currency VARCHAR(32)
)

CREATE TABLE flower_bucket_flower (
	id INT IDENTITY(1,1) PRIMARY KEY,
	flower_id INT FOREIGN KEY REFERENCES flower(id),
	bucket_id INT FOREIGN KEY REFERENCES flower_bucket(id)
)

IF EXISTS (
        SELECT type_desc, type
        FROM sys.procedures WITH(NOLOCK)
        WHERE NAME = 'get_top5_expensive_flowers'
            AND type = 'P'
      )
DROP PROCEDURE get_top5_expensive_flowers
