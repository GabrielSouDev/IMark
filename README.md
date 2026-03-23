# IMark

Aplicação multiplataforma de registro de ponto desenvolvida em .NET MAUI Blazor Hybrid,
com suporte a Android, iOS e Web. Integrada ao sistema central de gestão de
Recursos Humanos [IManager](https://github.com/GabrielSouDev/IManager).

---

## 🧱 Arquitetura

O projeto é composto por três camadas principais:

- **IMark.App** — Aplicação MAUI Blazor Hybrid (Android, iOS)
- **IMark.Shared** — Biblioteca Razor compartilhada com componentes, páginas e lógica comum
- **IMark.Web** — Aplicação Blazor WebAssembly (Web)

---

## ✨ Funcionalidades

- Registro de ponto (entrada, saída, intervalos)
- Autenticação segura via JWT
- Visualização do histórico de registros
- Interface responsiva e multiplataforma

---

## 🛠️ Tecnologias

- [.NET MAUI Blazor Hybrid](https://learn.microsoft.com/aspnet/core/blazor/hybrid/)
- [Blazor WebAssembly](https://learn.microsoft.com/aspnet/core/blazor/webassembly/)
- Razor Class Library
- JWT para autenticação
- C# / Razor Components

---

## 📦 Pré-requisitos

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- Visual Studio 2022+ com workloads:
  - .NET MAUI
  - ASP.NET e desenvolvimento Web
- Para Android: Android SDK configurado
- Para iOS: macOS com Xcode (build remoto ou Mac)

---

## 🚀 Como executar

Clone o repositório:

```bash
git clone https://github.com/GabrielSouDev/IMark.git
cd IMark
```

### Web (Blazor WebAssembly)

dotnet run --project IMark.Web

### Mobile (Android/iOS)

Abra a solution no Visual Studio, selecione o projeto IMark.App,
escolha o dispositivo/emulador e execute com F5.

---

## 🔗 Integração

O IMark se comunica com a API do [IManager](https://github.com/GabrielSouDev/IManager),
sistema responsável pela gestão de RH, folha de pagamento e holerites.
A autenticação é realizada via token JWT fornecido pela API do IManager.