# PGen (WinForms) – AK/EK Generator

## Build & run

- Build:
  - `dotnet build .\PGen.sln`
- Run:
  - `dotnet run --project .\PGen\PGen.csproj`

## Licensing (MAC-locked)

This app checks for `license.bin` next to the built EXE **before** showing the main UI.

- **Create a local license for this machine** (writes `license.bin` to the output folder):
  - `dotnet run --project .\PGen\PGen.csproj -- --make-license`
  - Optional expiry days:
    - `dotnet run --project .\PGen\PGen.csproj -- --make-license --days 365`

If the license check fails, the app shows the **Machine ID** you need to allow-list.

## Using the app

- **MSN input**
  - Single value: `00001234`
  - Range: `00001234-00001300`
  - Only **8 / 10 / 12 digit** MSNs are accepted.
- **Generate**
  - Choose **Meter Type**, **Model**, and number of **Sets** (AK/EK pairs per MSN).
  - Results appear in the grid; you can copy a row or copy multiple selected rows.
- **Export**
  - **Export Excel (8)** exports `AK8/EK8` columns (first 8 hex chars of the 32-digit keys).
  - **Export Excel (32)** exports the full `AK32/EK32` columns.

## Database

- To prevent duplicate keys when saving, ensure the unique constraint exists:
  - Run `Scripts/add_meter_key_unique_constraint.sql` against your MySQL database.
  - Uniqueness is on (AK8, EK8, AK32, EK32). Duplicate key submissions overwrite existing records.

## Notes for production

- The current AK/EK algorithm is a deterministic HMAC-based implementation in:
  - `PGen\Services\PasswordGeneratorService.cs`
- Replace it with your validated production algorithm/keys.
- For stronger control, replace the offline `license.bin` flow with an online handshake (Web API) in:
  - `PGen\Security\LicenseValidator.cs`

