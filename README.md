# API Store NikTL 

## Set-up project
**1. Appseting**

Open file appseting.json in src/WebApi and put your informatio here, include your connect string, jwt key and mail setting, if you wan't infromation secret. Put it in secret file it will overide all in appsetiing

**2 Migrations**

Run code here in your command it auto make database

```GO
dotnet ef database update --project src/Infrastructure --startup-project src/WebApi
```

with in Infrastrucre contain Migration and in WebApi contrain connectString

### API EndPoint For Project

**1. Api for visiting guests**
 - GET /regrist : Regrist new account
 - GET /products : Get products with pagination
 - GET /products/search/: Get products has contrain name product with pagination
 - GET /promotions: Get promotions
 - GET /product/promotion: Get product has promotion id with pagination
 - GET /product/statisticalrating/: Get Statistical Rating for product
 - GET /product/ : Get product has Id
   
**2 APi for user**
- POST /login : User Login return new token with access token(Json Web Token) and refresh token for fe process and if user login fail more than 5 times user will wait 5 minute befor continue login avoid brute force atteck
- POST /logout: User logout recall refresh token = null
- POST /token: Get new token when access token has exprise
- POST /sendEmailConfirmAccount: server send token make user confirm account code 6 digital random and token has exprise after 2 minute
- POST /emailConfirm/: user input token make confirm account
- POST /changePassword : user change your passowrd
- POST /forgotPassword/ : user input your email and server will send token for user's email
- POST /resetPassword/: user input toke in action forgotPassword and server will send new password for user's email
- GET /userProfile: Get information for user
- PUT /updateProfile: Update information for user
- POST /cart/create: Create new order
- PUT /cart/update/: Update quantity and product value type in order
- DELETE /cart/delete/: Delete order in cart
- GET /cart/orders: Get orders in cart
- GET /ratings: Get ratings for user with paginattion
- GET /rating: Get rating for user in order detail
- POST /reaction: Create new reaction for rating
- PUT /reaction/update/: Update reaction
- DELETE /reaction/delete/: Delete Reaction

**3. API for admin**
- POST /product/create: Create new product
- DELETE /product/delete/: Delete product
- PUT /product/put/: Update all informtion for product
- PATCH /product/patch/: update some props in product
- POST /promotion/create: Create new promotion
- PUT /promotion/put/: Update put promotion has id
- DELETE /promotion/delete: Delete promotion has id
- PATCH /promotion/patch/: Update patch promotion has id
- POST /promotion/add: Add promotion for product
- DELETE /promotion/remove: Remove promotion for product
