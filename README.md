**dotnet ef migrations add WHY??? --context ApplicationDbContext**

**dotnet ef database update --context ApplicationDbContext**

**Business:** Her bir ugrastigin alanin ozel olarak kodunu yazdigin yer. Mediatr ile yaziyorsun.

==========================

**Repository:**
Database'e sorgu atilacaksa burada her bir model icin repository olusturulur. Repository uzerinden method cagrilarak yapilir.

-Dependency Injection'a eklemeyi unutma.

**Controller:**
Herhangi bir endpoint burada tanimlanir.

**Exceptions:**

- Yazilimci hataliysa:
  TechinalApplicationException'dan inherit edilerek specific exception'lar atilmai.
  bkz: PdfNotFoundException : TechincalApplicationException

- Kullanici hataliysa:
  BusinessApplicationException'dan inherit edilerek specific exception'lar atilmai.
  bkz: DuplicateEmailException : BusinessApplicationException

**Auth:**
Bir request'i bir policy veya role ile kisitlamak istiyorsan, Command veya Query'e Authorize attribute'u ile tanimlanmali.
[Authorize(Roles = "admin,worker", Policy = "onsekizplus,sevenminus")]

Bir user'in bir yerde kim oldugunu algilamak istiyorsan ICurrentUserProvider inject etmelisin.

Genel gelistirme =>

Bir seyin ne olduguna karar vermelisin.
Bir sey ya bir sey yapar, veyahut bir sey sorgular.
Command bir sey yapar ve muhtemel olarak donebilir.
Query bir sey doner.

```cs
public record LoginQuery(
    string Email,
    string Password) : IRequest<AuthenticationResult>;
```

LoginQuery tipinde bir message yaratilir.
IRequest'den inherit alir (mediatr library'si boyle calisiyor), ve AuthenticationResult tipinde bir result geri doner.

```cs
public record RegisterCommand(
    string FirstName,
    string LastName,
    string Email,
    string Password) : IRequest<AuthenticationResult>;
```

RegisterCommand tipinde bir message yaratilir.
IRequest'den inherit alir (mediatr library'si boyle calisiyor), ve AuthenticationResult tipinde bir result geri doner.

```cs
public class LoginQueryHandler : IRequestHandler<LoginQuery, AuthenticationResult>
{}
```

Bir QueryHandler kendi Query'sinin implementasyonudur. IRequestHandler<A,B>'dan inherit alir.

- A Message'in tipidir.
- B Handler'in donecegi result'in tipidir.

```cs
public class RegisterCommandHandler : IRequestHandler<RegisterCommand, AuthenticationResult>
{}
```

Bir CommandHandler kendi Command'inin implementasyonudur. IRequestHandler<A,B>'dan inherit alir.

- A Message'in tipidir.
- B Handler'in donecegi result'in tipidir.

```cs
public record RegisterCommand(
    string FirstName,
    string LastName,
    string Email,
    string Password) : IRequest;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand> {}
```

Bir CommandHandler kendi Command'inin implementasyonudur. Bir result geriye donulmeyecekse, IRequestHandler<A>'dan inherit alir.

- A Message'in tipidir.
