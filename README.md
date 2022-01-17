<div id="top"></div>

# Online Shop

<!-- TABLE OF CONTENTS -->
<details>
  <summary>Table of Contents</summary>
  <ol>
    <li>
      <a href="#about-the-project">About The Project</a>
      <ul>
        <li><a href="#main-features">Main Features</a></li>
        <li><a href="#integrated-services">Integrated Services</a></li>
        <li><a href="#built-with">Built With</a></li>
        <li><a href="#visuals">Visuals</a></li>
      </ul>
    </li>
    <li>
      <a href="#getting-started">Getting Started</a>
      <ul>
        <li><a href="#installation">Installation</a></li>
      </ul>
    </li>
    <li><a href="#usage">Usage</a></li>
    <li><a href="#development-team">Development Team</a></li>
    <li><a href="#acknowledgments">Acknowledgments</a></li>
  </ol>
</details>



<!-- ABOUT THE PROJECT -->
## About The Project

![home-page.png][home-page]

Codecool shop is an online shop you can use to buy gadgets like phones and tablets

- The user can sort the products by Category or Supplier
- The user can place an order with or without an account, by using a credit card
- The user receives an email confirmation for both user registration and placed order details

<p align="right">(<a href="#top">back to top</a>)</p>


### Main Features

- Sort Products by Category and Supplier
- Register
- Login/Logout
- Add to Cart
- Cart Preview
- Edit cart items quantity from the Cart Preview (Increase, Decrease, Remove)
- Place order
- User Dashboard - Placed orders details
- Event logging

<p align="right">(<a href="#top">back to top</a>)</p>

### Built With

Back End:
* [ASP .NET Core][asp-net-core]
* [Entity Framework Core][ef-core]
* [C#][c#]

Front End:
* [HTML][html]
* [CSS][css]
* [JavaScript][js]
* [React.js][react]
* [Bootstrap][bootstrap]
* [JQuery][jquery]

Database Management:
* [Microsoft SQL Server][msql-server]
* [Microsoft SQL Server Management Studio][ssms]

IDE:
* [Microsoft Visual Studio][visual-studio]

<p align="right">(<a href="#top">back to top</a>)</p>


### Integrated Services

Email:
* [Sendgrid][sendgrid]

Payment processing:
* [Stripe][stripe]

Event logging:
* [Serilog][serilog]

<p align="right">(<a href="#top">back to top</a>)</p>

### Visuals

Empty Cart:

![empty-cart.png][empty-cart]

Register Page:

![register-page.png][register-page]

Login Page:

![login-page.png][login-page]

Home Page while logged in**:**

![logged-in-home-page.png][logged-in-home-page]

Used Dashboard - Orders:

![user-dashboard.png][user-dashboard]

Filled Cart Preview from the Home Page:

![items-in-cart.png][items-in-cart]

Cart state before Checkout:

![pre-checkout-cart-preview.png][pre-checkout-cart-preview]

Delivery details form:

![delivery-details.png][delivery-details]

Successful Order Placement Notification Page:

![successful-order.png][successful-order]

Used Dashboard - Order Details:

![placed-order-details.png][placed-order-details]

<p align="right">(<a href="#top">back to top</a>)</p>


<!-- GETTING STARTED -->
## Getting Started

### Installation

- Create a database
- Fill in the Stripe - SecretKey, Sendgrid - ApiKey and ConnectionStrings - CodecoolShop fields in appsettings.js or paste the following structure in secrets.json file of the project:
    
    ```json
    {
      "ConnectionStrings": {
        "CodeCoolShop": "<your-database-connection-string-comes-here>",
      },
      "Stripe": {
        "SecretKey": "<your-stripe-secret-key-comes-here>"
      },
      "Sendgrid": {
        "ApiKey": "<your-sendgrid-api-key-comes-here>"
      }
    }
    ```
    
- Fill in other details related to Sendgrid and Stripe according to your data
- Update the database from the Package Manager Console

<p align="right">(<a href="#top">back to top</a>)</p>



<!-- USAGE EXAMPLES -->
## Usage

Run the project with Kestrel Server to get events logged with Serilog.

<p align="right">(<a href="#top">back to top</a>)</p>


## Development Team

* [Mihai Buga's GitHub][mihai-buga]
* [Nicolae Peptea's GitHub][nicolae-peptea]

<p align="right">(<a href="#top">back to top</a>)</p>

<!-- ACKNOWLEDGMENTS -->
## Acknowledgments

Thanks for all the support to the Codecool mentors that have guided us!

[React.Js for ASP .NET MVC][react-net]

<p align="right">(<a href="#top">back to top</a>)</p>



<!-- MARKDOWN LINKS & IMAGES -->
[contributors-shield]: https://img.shields.io/github/contributors/othneildrew/Best-README-Template.svg?style=for-the-badge
[contributors-url]: https://github.com/mihaibuga/online-shop/graphs/contributors
[linkedin-shield]: https://img.shields.io/badge/-LinkedIn-black.svg?style=for-the-badge&logo=linkedin&colorB=555
[linkedin-url]: https://www.linkedin.com/in/mihai-buga

[asp-net-core]: https://dotnet.microsoft.com/en-us/learn/aspnet/what-is-aspnet-core
[ef-core]: https://docs.microsoft.com/en-us/ef/core/
[c#]: https://docs.microsoft.com/en-us/dotnet/csharp/
[html]: https://html.com/
[css]: https://www.w3.org/Style/CSS/Overview.en.html
[js]: https://www.javascript.com/
[react]: https://reactjs.org/
[react-net]: https://reactjs.net/
[bootstrap]: https://getbootstrap.com
[jquery]: https://jquery.com
[msql-server]: https://www.microsoft.com/en-us/sql-server/sql-server-2019
[ssms]: https://docs.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms?view=sql-server-ver15
[visual-studio]: https://visualstudio.microsoft.com/

[mihai-buga]: https://github.com/mihaibuga
[nicolae-peptea]: https://github.com/Nicolae-Peptea

[sendgrid]: https://sendgrid.com/
[stripe]: https://stripe.com/
[serilog]: https://serilog.net/

[home-page]: src/Codecool.CodecoolShop/wwwroot/img/captures/home-page.png
[empty-cart]: https://res.cloudinary.com/dqwtm9fw1/image/upload/v1642429623/CodeCoolShop/empty-cart_mjprbo.png
[register-page]: src/Codecool.CodecoolShop/wwwroot/img/captures/register-page.png
[login-page]: src/Codecool.CodecoolShop/wwwroot/img/captures/login-page.png
[logged-in-home-page]: src/Codecool.CodecoolShop/wwwroot/img/captures/logged-in-home-page.png
[user-dashboard]: src/Codecool.CodecoolShop/wwwroot/img/captures/user-dashboard.png
[items-in-cart]: src/Codecool.CodecoolShop/wwwroot/img/captures/items-in-cart.png
[pre-checkout-cart-preview]: src/Codecool.CodecoolShop/wwwroot/img/captures/pre-checkout-cart-preview.png
[delivery-details]: src/Codecool.CodecoolShop/wwwroot/img/captures/delivery-details.png
[successful-order]: src/Codecool.CodecoolShop/wwwroot/img/captures/successful-order.png
[placed-order-details]: src/Codecool.CodecoolShop/wwwroot/img/captures/placed-order-details.png
