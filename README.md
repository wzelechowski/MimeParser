# MimeParser API

MimeParser to wydajne i skalowalne REST API stworzone w technologii **C# (.NET 8)** z wykorzystaniem architektury **Minimal APIs**. Aplikacja służy do dekodowania danych zakodowanych w **Base64** oraz parsowania różnych formatów ładunków (m.in. CSV oraz JSON).

---

## 🛠️ Główne cechy

- **.NET 8 Minimal APIs** – lekka i wydajna architektura endpointów.
- **Strategy Pattern** – logika parsowania poszczególnych formatów (CSV, JSON) została wydzielona do osobnych strategii.
- **Keyed Services (.NET 8)** – dynamiczne, silnie typowane wstrzykiwanie zależności na podstawie `ParseType`.
- **Result Pattern** – obsługa błędów biznesowych bez nadużywania wyjątków (`throw`).
- **Global Exception Handler (`IExceptionHandler`)** – centralna obsługa wyjątków i mapowanie błędów do standardu **RFC 7807 Problem Details**.
- **Docker** – zoptymalizowany wieloetapowy `Dockerfile` zgodny z rekomendacjami Microsoft dla .NET 8 (uruchamianie jako non-root).

---

## 📋 Wymagania

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- Docker Desktop *(opcjonalnie)*

---

# 🐳 Uruchomienie w Dockerze

### 1. Przejdź do katalogu projektu

```bash
cd MimeParser
```

### 2. Zbuduj obraz

```bash
docker build -t mime-parser-api .
```

### 3. Uruchom kontener

```bash
docker run -p 8080:8080 \
-e ASPNETCORE_ENVIRONMENT=Development \
--name mime-parser \
mime-parser-api
```

### Swagger

```
http://localhost:8080/swagger
```

---

# 🚀 Uruchomienie lokalne

### Przywrócenie pakietów

```bash
dotnet restore
```

### Uruchomienie aplikacji

```bash
dotnet run
```

lub z Hot Reload:

```bash
dotnet watch
```

### Swagger

```
http://localhost:5017/swagger
```

> Jeśli aplikacja uruchomi się na innym porcie, zostanie on wyświetlony w konsoli.

---

# 📡 API

## Endpoint

```
POST /api/v1/parse-content
```

### Headers

```
Content-Type: application/json
```

---

# 📄 Przykład — Parsowanie CSV

## Request

```json
{
  "type": "CSV",
  "content": "aWQsbmFtZSxyb2xlCjEsV2lrdG9yLEJhY2tlbmQgRGV2ZWxvcGVyCjIsQW5uYSxGcm9udGVuZCBEZXZlbG9wZXIKMyxKYW4sRGV2T3BzIEVuZ2luZWVy"
}
```

## Response (200 OK)

```json
{
  "status": "Success",
  "processedCount": 3,
  "data": [
    {
      "id": "1",
      "name": "Wiktor",
      "role": "Backend Developer"
    },
    {
      "id": "2",
      "name": "Anna",
      "role": "Frontend Developer"
    },
    {
      "id": "3",
      "name": "Jan",
      "role": "DevOps Engineer"
    }
  ]
}
```

---

# ❌ Przykład błędu walidacji

## Response (400 Bad Request)

```json
{
  "type": "https://tools.ietf.org/html/rfc9110#section-15.5.1",
  "title": "Request validation error",
  "status": 400,
  "detail": "Failed to read parameter \"ParseRequest payload\" from the request body as JSON."
}
```

---

# 🏗️ Wykorzystane technologie

- C#
- .NET 8
- ASP.NET Core Minimal APIs
- Dependency Injection
- Keyed Services
- Strategy Pattern
- Result Pattern
- Docker
- Swagger / OpenAPI
