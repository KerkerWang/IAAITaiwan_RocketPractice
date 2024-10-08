![縱火調查協會封面](https://github.com/user-attachments/assets/4f3b207e-2110-42c2-a8eb-eebed5b0c485)
# 縱火調查協會官網 <img alt="MVC5" src="https://img.shields.io/badge/.NET_Framework-MVC_5-Green">
本專案以 C# 語言進行撰寫，使用 ASP.NET MVC 進行網站前後台開發。  
前台使用者除了可以進行頁面瀏覽外，會員能在登入後於留言板進行討論。  
後台管理者能以 CKEditor 進行網頁內容撰寫，並對於資料進行新增、修改、刪除等管理。

## 功能介紹
系統提供三種角色，分別對應不同的功能操作。  
### 一般使用者
- 瀏覽網站內容
- 前台會員註冊與登入
- 填寫表單通知網站管理者

### 前台會員
- 具備「一般使用者」的所有功能
- 修改個人會員資料
- 在討論區發表留言與回覆

### 後台管理者
- 編輯與管理網站內容
- 新增、修改、刪除各類數據
- 管理後台管理者的權限

## 畫面
### 討論區
<div><img width="50% alt="討論區" src="https://github.com/user-attachments/assets/fbfb985d-8d83-4e3e-8d74-9dc8cdd9768a"/></div>

### 聯絡我們  
<div><img width="50% alt="聯絡我們" src="https://github.com/user-attachments/assets/01fac329-bc53-4f31-b1ec-6b0e198f16f0"/></div>

### 後台編輯頁面
<div><img width="50% alt="後台編輯頁面" src="https://github.com/user-attachments/assets/aa909483-1467-433e-a6f4-5d1e32e97a92"/></div>

## 技術與工具介紹

### 開發環境
- 框架：.NET Framework 4.8
- 專案類型：ASP.NET MVC 5
### 技術使用
<div>
<img alt="Visual_Studio" src="https://img.shields.io/badge/Visual_Studio-5C2D91?style=for-the-badge&logo=visual%20studio&logoColor=white" />
<img alt=".NET" src="https://img.shields.io/badge/.NET-512BD4?style=for-the-badge&logo=dotnet&logoColor=white" />
<img alt="C#" src="https://img.shields.io/badge/c%23-%23239120.svg?style=for-the-badge&logo=csharp&logoColor=white" />
<img alt="SQL" src="https://img.shields.io/badge/Microsoft%20SQL%20Server-CC2927?style=for-the-badge&logo=microsoft%20sql%20server&logoColor=white" />
<div/>
<div>  
<img alt="Entity_Framework" src="https://img.shields.io/badge/Entity_Framework-yellow?style=for-the-badge">
<img alt="LINQ" src="https://img.shields.io/badge/LINQ-8A2BE2?style=for-the-badge">
<img alt="JQuery" src="https://img.shields.io/badge/jquery-%230769AD.svg?style=for-the-badge&logo=jquery&logoColor=white">
<img alt="Bootstrap" src="https://img.shields.io/badge/bootstrap-%238511FA.svg?style=for-the-badge&logo=bootstrap&logoColor=white">
</div>
<br/>

- 區域路由：透過 Areas 建立後台模組，並實現前台與後台的會員權限分離
- 資料庫存取：Microsoft SQL Server 搭配 Entity Framework Code First 以及 LINQ 進行資料庫存取
- 權限控管：透過自定義篩選器（Custom Filter）實現對特定 Controller 的權限管理 
- 遞迴函式：使用遞迴函式自動生成 Navbar 與 Sidebar
