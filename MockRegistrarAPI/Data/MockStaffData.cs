using MockRegistrarAPI.Models;

namespace MockRegistrarAPI.Data;

public static class MockStaffData
{
    public static List<RegistrarStaff> Staff { get; } =
        new List<RegistrarStaff>
        {
            new RegistrarStaff
            {
                EmployeeId = "EMP-4K-001",
                FullName = "Abebe Tadesse",
                PhoneNumber = "0911000001",
                Campus = "4K",
                Role = "Manager",
                AssignedBlock = null,
                Status = 1
            },

            new RegistrarStaff
            {
                EmployeeId = "EMP-4K-002",
                FullName = "Hana Bekele",
                PhoneNumber = "0911000002",
                Campus = "4K",
                Role = "Proctor",
                AssignedBlock = "Male Dormitory",
                Status = 1
            },

            new RegistrarStaff
            {
                EmployeeId = "EMP-4K-003",
                FullName = "Dawit Alemu",
                PhoneNumber = "0911000003",
                Campus = "4K",
                Role = "Maintenance",
                AssignedBlock = null,
                Status = 1
            },


            // ==========================================
            // CAMPUS 5K
            // ==========================================

            new RegistrarStaff
            {
                EmployeeId = "EMP-5K-001",
                FullName = "Meron Kebede",
                PhoneNumber = "0911000004",
                Campus = "5K",
                Role = "Manager",
                AssignedBlock = null,
                Status = 1
            },

            new RegistrarStaff
            {
                EmployeeId = "EMP-5K-002",
                FullName = "Samuel Girma",
                PhoneNumber = "0911000005",
                Campus = "5K",
                Role = "Proctor",
                AssignedBlock = "Female Dormitory",
                Status = 1
            },

            new RegistrarStaff
            {
                EmployeeId = "EMP-5K-003",
                FullName = "Rahel Worku",
                PhoneNumber = "0911000006",
                Campus = "5K",
                Role = "Maintenance",
                AssignedBlock = null,
                Status = 1
            },


            // ==========================================
            // CAMPUS 6K
            // ==========================================

            new RegistrarStaff
            {
                EmployeeId = "EMP-6K-001",
                FullName = "Yonas Haile",
                PhoneNumber = "0911000007",
                Campus = "6K",
                Role = "Manager",
                AssignedBlock = null,
                Status = 1
            },

            new RegistrarStaff
            {
                EmployeeId = "EMP-6K-002",
                FullName = "Selam Tesfaye",
                PhoneNumber = "0911000008",
                Campus = "6K",
                Role = "Proctor",
                AssignedBlock = "Male Dormitory",
                Status = 1
            },

            new RegistrarStaff
            {
                EmployeeId = "EMP-6K-003",
                FullName = "Bereket Mekonnen",
                PhoneNumber = "0911000009",
                Campus = "6K",
                Role = "Maintenance",
                AssignedBlock = null,
                Status = 1
            },


            // ==========================================
            // SEFERSELAM CAMPUS
            // ==========================================

            new RegistrarStaff
            {
                EmployeeId = "EMP-SEF-001",
                FullName = "Liya Getachew",
                PhoneNumber = "0911000010",
                Campus = "Seferselam",
                Role = "Manager",
                AssignedBlock = null,
                Status = 1
            },

            new RegistrarStaff
            {
                EmployeeId = "EMP-SEF-002",
                FullName = "Daniel Abebe",
                PhoneNumber = "0911000011",
                Campus = "Seferselam",
                Role = "Proctor",
                AssignedBlock = "Female Dormitory",
                Status = 1
            },

            new RegistrarStaff
            {
                EmployeeId = "EMP-SEF-003",
                FullName = "Ruth Mekonnen",
                PhoneNumber = "0911000012",
                Campus = "Seferselam",
                Role = "Maintenance",
                AssignedBlock = null,
                Status = 1
            },


            // ==========================================
            // COMMERCE CAMPUS
            // ==========================================

            new RegistrarStaff
            {
                EmployeeId = "EMP-COM-001",
                FullName = "Kalkidan Tesfaye",
                PhoneNumber = "0911000013",
                Campus = "Commerce",
                Role = "Manager",
                AssignedBlock = null,
                Status = 1
            },

            new RegistrarStaff
            {
                EmployeeId = "EMP-COM-002",
                FullName = "Nahom Bekele",
                PhoneNumber = "0911000014",
                Campus = "Commerce",
                Role = "Proctor",
                AssignedBlock = "Male Dormitory",
                Status = 1
            },

            new RegistrarStaff
            {
                EmployeeId = "EMP-COM-003",
                FullName = "Mekdes Alemu",
                PhoneNumber = "0911000015",
                Campus = "Commerce",
                Role = "Maintenance",
                AssignedBlock = null,
                Status = 1
            },


            // ==========================================
            // TIKUR ANBESSA CAMPUS
            // ==========================================

            new RegistrarStaff
            {
                EmployeeId = "EMP-TKA-001",
                FullName = "Yonas Girma",
                PhoneNumber = "0911000016",
                Campus = "Tikur Anbessa",
                Role = "Manager",
                AssignedBlock = null,
                Status = 1
            },

            new RegistrarStaff
            {
                EmployeeId = "EMP-TKA-002",
                FullName = "Selamawit Haile",
                PhoneNumber = "0911000017",
                Campus = "Tikur Anbessa",
                Role = "Proctor",
                AssignedBlock = "Female Dormitory",
                Status = 1
            },

            new RegistrarStaff
            {
                EmployeeId = "EMP-TKA-003",
                FullName = "Eyob Worku",
                PhoneNumber = "0911000018",
                Campus = "Tikur Anbessa",
                Role = "Maintenance",
                AssignedBlock = null,
                Status = 1
            },


            // ==========================================
            // FBE CAMPUS
            // ==========================================

            new RegistrarStaff
            {
                EmployeeId = "EMP-FBE-001",
                FullName = "Rahel Kebede",
                PhoneNumber = "0911000019",
                Campus = "FBE",
                Role = "Manager",
                AssignedBlock = null,
                Status = 1
            },

            new RegistrarStaff
            {
                EmployeeId = "EMP-FBE-002",
                FullName = "Samuel Tadesse",
                PhoneNumber = "0911000020",
                Campus = "FBE",
                Role = "Proctor",
                AssignedBlock = "Male Dormitory",
                Status = 1
            },

            new RegistrarStaff
            {
                EmployeeId = "EMP-FBE-003",
                FullName = "Hana Mekonnen",
                PhoneNumber = "0911000021",
                Campus = "FBE",
                Role = "Maintenance",
                AssignedBlock = null,
                Status = 1
            }
        };
}