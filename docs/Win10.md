# 使用windows 10安装与部署智能仓储配送系统（Wms）

一、IIS安装与配置

安装IIS时注意，打开或关闭windows功能，.NET Framework4.7 高级服务-WCF服务，需选中HTTP激活。

1、输入inetmgr，打开IIS管理器

2、右键应用程序池-添加应用程序池，名称输入：.NET v4.0，点击“确定”，选中 .NET v4.0应用程序池，点击“高级设置”，“标识”-内置账户，选择“NetworkService”，如图：

3、鼠标定位在默认站点，右键-添加应用程序， 
别名：Wms， 应用程序池 .NET v4.0 ，选择物理路径后，点击“确定”按钮。

![iis](../images/iis001.png)

![iis](../images/iis002.png)

![iis](../images/iis003.png)

![iis](../images/iis004.png)

![iis](../images/iis005.png)


二、数据库安装与配置

1、打开Microsoft SQL Server Management Studio，分别附加TygaSoftAspnetDb、 WmsDb

2、分别在TygaSoftAspnetDb、WmsDb创建用户Network Service,并分配db_owner权限

![sqlserver](../images/SqlServer001.png)

![sqlserver](../images/SqlServer002.png)

![sqlserver](../images/SqlServer003.png)


至此，打开浏览器，输入：http://localhost/Asset 回车，输入登录账号并登录，即可进入后台管理页。

![web](../images/wms001.png)