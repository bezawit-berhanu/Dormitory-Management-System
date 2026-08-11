using DormitoryManagementSystem.Domain.Entities;

namespace DormitoryManagementSystem.Domain.Interfaces;

public interface ICheckOutRepository
{

    Task<IEnumerable<CheckOut>> GetAllAsync();


    Task<CheckOut?> GetByIdAsync(int id);


    Task<IEnumerable<CheckOut>> GetByStudentIdAsync(int studentId);


    Task<CheckOut> AddAsync(CheckOut checkOut);


    Task UpdateAsync(CheckOut checkOut);


    Task DeleteAsync(int id);


    Task SaveAsync();

}