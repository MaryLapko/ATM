CREATE PROCEDURE get_top5_expensive_flowers
@name varchar(255)
AS
BEGIN
SELECT top 5 *
FROM flower
WHERE name = @name
ORDER BY price DESC
END