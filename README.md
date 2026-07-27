[TOC]

# MGS.Komodo

## Summary

- Silent license controller for Unity project.

## Ability

- Verify license automatically.
- Create license request file and restrict app if verify failed.

## Install

- Unity --> Window --> Package Manager --> "+" --> Add package from git URL...

  ```text
  https://github.com/mogoson/MGS.License.git?path=/Assets
  https://github.com/mogoson/MGS.Komodo.git?path=/Assets
  ```
## Usage

- Unity menu "Toos/License/Settings" to set license parameters.
  - Should not change (Re Generate) the keys after project published.
- Unity menu "Toos/License/Builder" to build license.
  - Trial button to get a license request text for trial.
  - The license build from trial request without bind device.
- Save trial license file named {productName}.lic to streamingAssetsPath.
- Find the request file named {productName}.lre at path persistentDataPath if trial license is expired.
- Build license base on the request file and Save license file named {productName}.lic to persistentDataPath.

------

Copyright © 2026 Mogoson.	mogoson@outlook.com