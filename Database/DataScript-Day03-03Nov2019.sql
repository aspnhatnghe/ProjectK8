INSERT [dbo].[Category] ([CategoryId], [CategoryName], [Description], [ParentCategoryId]) VALUES (N'1dce10e3-8432-4b96-93bd-f3160973af4b', N'Balo', N'<p>Balo<br></p>', NULL)
INSERT [dbo].[Category] ([CategoryId], [CategoryName], [Description], [ParentCategoryId]) VALUES (N'52a3e97e-63dd-44a6-a4a4-45b378c0d7c7', N'Phụ kiện', N'<p>Phụ kiện<br></p>', NULL)
INSERT [dbo].[Category] ([CategoryId], [CategoryName], [Description], [ParentCategoryId]) VALUES (N'553e9306-1b4e-42fd-8d4e-7f495fb072d7', N'Tablet', N'<p>Tablet<br></p>', NULL)
INSERT [dbo].[Category] ([CategoryId], [CategoryName], [Description], [ParentCategoryId]) VALUES (N'6a90c7a8-89c2-4ba7-84da-cd387976e5dd', N'Điện thoại', N'<p>Điện thoại<br></p>', NULL)
INSERT [dbo].[Category] ([CategoryId], [CategoryName], [Description], [ParentCategoryId]) VALUES (N'7760a0e3-b7ca-4dec-8b4e-6afeac082480', N'Máy ảnh', N'<p>Máy ảnh<br></p>', NULL)
INSERT [dbo].[Category] ([CategoryId], [CategoryName], [Description], [ParentCategoryId]) VALUES (N'8a989716-d041-45e8-9edf-f06534335955', N'Laptop', N'<p>Laptop<br></p>', NULL)
INSERT [dbo].[Category] ([CategoryId], [CategoryName], [Description], [ParentCategoryId]) VALUES (N'd022a411-cf59-4b58-9447-0ccd922b6e6b', N'Túi xách', N'<p>Túi xách<br></p>', NULL)
INSERT [dbo].[Category] ([CategoryId], [CategoryName], [Description], [ParentCategoryId]) VALUES (N'd660f75c-a423-4cb9-aec2-c42e1a80fe56', N'Đồng hồ thời trang', N'<p>Đồng hồ thời trang<br></p>', N'e0037927-a0f9-4405-aae3-07a95f498c49')
INSERT [dbo].[Category] ([CategoryId], [CategoryName], [Description], [ParentCategoryId]) VALUES (N'dbbec2c3-8226-42a6-b9e4-6114e2c75599', N'Đồng hồ thông minh', N'<p>Đồng hồ thông minh<br></p>', N'e0037927-a0f9-4405-aae3-07a95f498c49')
INSERT [dbo].[Category] ([CategoryId], [CategoryName], [Description], [ParentCategoryId]) VALUES (N'e0037927-a0f9-4405-aae3-07a95f498c49', N'Đồng hồ - Mắt kính', N'<p>Đồng hồ - Mắt kính<br></p>', NULL)
INSERT [dbo].[Category] ([CategoryId], [CategoryName], [Description], [ParentCategoryId]) VALUES (N'fb0b4209-b70b-46cc-b2b1-5b137fe7a31c', N'Mắt kính thời trang', N'<p>Mắt kính thời trang<br></p>', N'e0037927-a0f9-4405-aae3-07a95f498c49')
SET IDENTITY_INSERT [dbo].[Supplier] ON 

INSERT [dbo].[Supplier] ([SupplierId], [SupplierName], [Address], [Phone], [Logo]) VALUES (1, N'Samsung', N'CNC Q9', NULL, NULL)
INSERT [dbo].[Supplier] ([SupplierId], [SupplierName], [Address], [Phone], [Logo]) VALUES (2, N'Nokia', N'Nokia', NULL, NULL)
INSERT [dbo].[Supplier] ([SupplierId], [SupplierName], [Address], [Phone], [Logo]) VALUES (3, N'BlackBerry', N'BlackBerry', NULL, NULL)
INSERT [dbo].[Supplier] ([SupplierId], [SupplierName], [Address], [Phone], [Logo]) VALUES (4, N'VSmart', N'VMart', NULL, NULL)
INSERT [dbo].[Supplier] ([SupplierId], [SupplierName], [Address], [Phone], [Logo]) VALUES (5, N'Apple', N'Apple', NULL, NULL)
INSERT [dbo].[Supplier] ([SupplierId], [SupplierName], [Address], [Phone], [Logo]) VALUES (6, N'Sony', N'Sony', NULL, NULL)
INSERT [dbo].[Supplier] ([SupplierId], [SupplierName], [Address], [Phone], [Logo]) VALUES (7, N'Anker', N'Anker', NULL, NULL)
INSERT [dbo].[Supplier] ([SupplierId], [SupplierName], [Address], [Phone], [Logo]) VALUES (8, N'Logitech', N'Logitech', NULL, NULL)
INSERT [dbo].[Supplier] ([SupplierId], [SupplierName], [Address], [Phone], [Logo]) VALUES (9, N'OPPO', N'OPPO', NULL, NULL)
INSERT [dbo].[Supplier] ([SupplierId], [SupplierName], [Address], [Phone], [Logo]) VALUES (10, N'XIAOMI', N'XIAOMI', NULL, NULL)
INSERT [dbo].[Supplier] ([SupplierId], [SupplierName], [Address], [Phone], [Logo]) VALUES (11, N'VIVO', N'VIVO', NULL, NULL)
INSERT [dbo].[Supplier] ([SupplierId], [SupplierName], [Address], [Phone], [Logo]) VALUES (12, N'LENOVO', N'LENOVO', NULL, NULL)
INSERT [dbo].[Supplier] ([SupplierId], [SupplierName], [Address], [Phone], [Logo]) VALUES (13, N'MOBELL', N'MOBELL', NULL, NULL)
INSERT [dbo].[Supplier] ([SupplierId], [SupplierName], [Address], [Phone], [Logo]) VALUES (14, N'ASUS', N'ASUS', NULL, NULL)
INSERT [dbo].[Supplier] ([SupplierId], [SupplierName], [Address], [Phone], [Logo]) VALUES (15, N'HP', N'HP', NULL, NULL)
INSERT [dbo].[Supplier] ([SupplierId], [SupplierName], [Address], [Phone], [Logo]) VALUES (16, N'DELL', N'DELL', NULL, NULL)
INSERT [dbo].[Supplier] ([SupplierId], [SupplierName], [Address], [Phone], [Logo]) VALUES (17, N'ACER', N'ACER', NULL, NULL)
INSERT [dbo].[Supplier] ([SupplierId], [SupplierName], [Address], [Phone], [Logo]) VALUES (18, N'MSI', N'MSI', NULL, NULL)
INSERT [dbo].[Supplier] ([SupplierId], [SupplierName], [Address], [Phone], [Logo]) VALUES (19, N'ROLEX', N'ROLEX', NULL, NULL)
INSERT [dbo].[Supplier] ([SupplierId], [SupplierName], [Address], [Phone], [Logo]) VALUES (20, N'CASIO', N'CASIO', NULL, NULL)
INSERT [dbo].[Supplier] ([SupplierId], [SupplierName], [Address], [Phone], [Logo]) VALUES (21, N'CENTURY', N'CENTURY', NULL, NULL)
INSERT [dbo].[Supplier] ([SupplierId], [SupplierName], [Address], [Phone], [Logo]) VALUES (22, N'Calvin Klein', N'Calvin Klein', NULL, NULL)
SET IDENTITY_INSERT [dbo].[Supplier] OFF