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
- Unity menu "Toos/License/Builder" to build license.
- Save license file to Application.streamingAssetsPath.
- Update license file to Application.persistentDataPath if previous license invalid.

------

Copyright © 2026 Mogoson.	mogoson@outlook.com