# 🛒 eShop.Web — Ứng dụng Thương mại điện tử

## 👤 Thông tin sinh viên

| Thông tin | Chi tiết |
|-----------|----------|
| **Họ và tên** | Huỳnh Thế Trung |
| **Mã sinh viên** | 23K4080053 |

---

## 📌 Giới thiệu dự án

**eShop.Web** là một ứng dụng thương mại điện tử được xây dựng bằng **Blazor (.NET)**, áp dụng kiến trúc **Clean Architecture** nhằm tách biệt rõ ràng các tầng nghiệp vụ, giao diện và dữ liệu.

---

## 🏗️ Kiến trúc dự án

```
eShop.Web/
├── eShop/                        # Ứng dụng Blazor chính
├── eShop.CoreBusiness/           # Domain Models & Business Logic
│   ├── Models/                   # Order, OrderLineItem, Product
│   └── Services/                 # IOrderService, OrderService
├── eShop.UseCases/               # Application Use Cases
│   ├── SearchProductScreen/
│   ├── ViewProductScreen/
│   ├── ShoppingCartScreen/
│   ├── OrderConfirmationScreen/
│   └── PluginInterface/          # Interfaces cho DataStore & UI
├── eShop.Web.Modules/            # UI Components (Razor Class Libraries)
│   ├── eShop.Web.CustomerPortal/ # Giao diện khách hàng
│   ├── eShop.Web.AdminPortal/    # Giao diện quản trị
│   └── eShop.Web.Common/        # Components dùng chung
└── Plugins/                      # Triển khai cụ thể (Data, State)
    ├── eShop.DataStore.HardCoded/
    └── eShop.ShoppingCartLocal/
```

---

## ⚙️ Công nghệ sử dụng

- **Framework**: ASP.NET Core / Blazor Server
- **Ngôn ngữ**: C#
- **Kiến trúc**: Clean Architecture
- **UI**: Razor Components, Bootstrap
- **State Management**: Local Storage

---

## 🚀 Hướng dẫn chạy dự án

```bash
# Clone repo
git clone https://github.com/trunghuynh1520-coder/eShop.Web.git

# Di chuyển vào thư mục
cd eShop.Web/eShop/eShop

# Chạy ứng dụng
dotnet run
```

---

## ✨ Chức năng chính

- 🔍 Tìm kiếm sản phẩm
- 🛍️ Xem chi tiết sản phẩm
- 🛒 Giỏ hàng (thêm, xoá, cập nhật số lượng)
- 📦 Đặt hàng
- ✅ Xác nhận đơn hàng
