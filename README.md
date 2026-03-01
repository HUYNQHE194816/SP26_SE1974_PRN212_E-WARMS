# SP26_SE1974_PRN212_E-WARMS
A C# OOP-based management system for an electronic device warranty and repair center. The project models customers, devices, technicians, service orders, and inventory to streamline operations such as check-in, diagnostics, repair tracking, invoicing, and reporting.

# **Warranty & Repair Center Management System – C# OOP**

## **📌 Overview**

This project is a **C# Object-Oriented Programming (OOP)** application designed to support the workflow of an **electronic device warranty and repair center**.
The system models real-world business entities such as customers, devices, technicians, repair orders, warranty tickets, and spare-part inventory.

The goal is to provide a clean, maintainable, and extensible architecture using core OOP principles:
**Encapsulation, Abstraction, Inheritance, and Polymorphism**.

---

## **✨ Features**

### **🔹 Customer & Device Management**

* Store customer information
* Track device details and repair history
* Support multiple devices per customer

### **🔹 Repair Workflow**

* Create service/repair tickets
* Assign technicians
* Update repair status (Pending → Diagnosing → Repairing → Completed)
* Add diagnostic notes and labor cost

### **🔹 Warranty Management**

* Register warranty information
* Generate warranty documents
* Track warranty expiration

### **🔹 Spare Parts & Inventory**

* Manage replacement parts
* Track stock levels
* Automatically calculate part costs in repair orders

### **🔹 Invoice & Billing**

* Generate invoices for repairs
* Calculate total cost (labor + parts)
* Export/print invoice (optional extension)

### **🔹 Reporting & Statistics**

* Daily/weekly/monthly revenue
* Technician performance
* Common repair types and parts usage

---

## **🧱 Project Structure**

Example folder layout (you can adjust as needed):

```
/WarrantyRepairCenter
│
├── Models
│   ├── Customer.cs
│   ├── Device.cs
│   ├── Technician.cs
│   ├── RepairOrder.cs
│   ├── WarrantyTicket.cs
│   └── SparePart.cs
│
├── Services
│   ├── CustomerService.cs
│   ├── RepairService.cs
│   ├── InventoryService.cs
│   └── BillingService.cs
│
├── Database
│   └── DataContext.cs
│
├── Utils
│   └── Helpers.cs
│
├── Program.cs
└── README.md
```

---

## **🧩 Technologies Used**

* **C# (.NET)**
* **Object-Oriented Programming**
* (Optional) **SQL Server / SQLite** for data persistence
* (Optional) **WinForms / WPF / Console App** UI

---

## **📘 OOP Concepts Applied**

### **✔ Encapsulation**

Sensitive data is protected through private fields and public properties.

### **✔ Inheritance**

Example: `ElectronicDevice → Phone, Laptop, Tablet`.

### **✔ Polymorphism**

Repair cost calculation can vary by device type or repair method.

### **✔ Abstraction**

Services hide complex logic such as billing or inventory processing.

---

## **📄 Future Improvements**

* Add graphical UI (WPF or WinForms)
* Cloud database integration
* Barcode/QR scanning for device check-in
* Multi-branch repair center management
* Export reports to PDF / Excel

---

## **🤝 Contributing**

Feel free to fork this repository and submit pull requests.
Bug reports and feature suggestions are welcome!

---

## **📜 License**

This project is released under the **MIT License**.

---
hoặc thậm chí tạo toàn bộ **skeleton project** cho bạn.
