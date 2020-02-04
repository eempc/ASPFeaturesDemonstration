# ASPFeaturesDemonstration
How to do common things on web apps

random bit of info

# To add custom Account Manage page do the following:

1. Scaffold identity for Manage, ManageNav and I think something else
2. Add 3 new things

* In Areas\identity\Pages\Account\Manage, add new razor page MyOrders.cshtml and also add:
```
@page
@model CarProject.Areas.Identity.Pages.Account.Manage.MyOrdersModel
@{
    ViewData["Title"] = "My Orders Page";
    ViewData["ActivePage"] = ManageNavPages.MyOrders;
}

<h4>@ViewData["Title"]</h4>
```

* in ManageNavPages.cs add two new lines
```
public static string MyOrders => "MyOrders"; // Custom page - view my orders
public static string MyOrdersNavClass(ViewContext viewContext) => PageNavClass(viewContext, MyOrders);
```
* in _ManageNav.cshtml
`<li class="nav-item"><a class="nav-link @ManageNavPages.MyOrdersNavClass(ViewContext)" id="my-orders" asp-page="./MyOrders">My Orders</a></li>`
