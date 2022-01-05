================================================================================================================================================================
SqlTableDependencyFilter NuGet Package
================================================================================================================================================================
Copyright (c) 2017 Christian Del Bianco 
Home site: https://sqltabledependencywhere.codeplex.com/
Documentation: https://sqltabledependencywhere.codeplex.com/documentation


---------------------------------------------------------------------------------------------------------------------------------------------------------------
Release 1.2.0.0
---------------------------------------------------------------------------------------------------------------------------------------------------------------
Breaking changes. Replaced IDictionary with IModelToTableMapper<T>:

CREATE TABLE [dbo].[Products](
	[Id] [int] NOT NULL,
	[CategoryId] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](50) NULL,
	[Quantity] [int] NULL)

public class Product
{
    public int Id { get; set; }
    public int CategoryId { get; set; }
    public string Name { get; set; }
    public int ItemsInStock { get; set; }
}

...

var mapper = new ModelToTableMapper<Product>();
mapper.AddMapping(c => c.ItemsInStock, "Quantity");

Expression<Func<Product, bool>> expression = p => (p.CategoryId == (int)CategorysEnum.Food || p.CategoryId == (int)CategorysEnum.Drink) && p.ItemsInStock <= 10;
ITableDependencyFilter whereCondition = new SqlTableDependencyFilter<Product>(expression, mapper);


---------------------------------------------------------------------------------------------------------------------------------------------------------------
Release 1.0.0.0
---------------------------------------------------------------------------------------------------------------------------------------------------------------
WHERE condition generator for SqlTableDependency trigger