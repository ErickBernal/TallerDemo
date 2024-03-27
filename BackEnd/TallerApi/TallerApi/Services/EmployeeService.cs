/*
using Microsoft.EntityFrameworkCore;
using TallerApi.Data.Models;
using TallerApi.Data;

namespace TallerApi.Services
{
    public class EmployeeService
    {
        private readonly DbebContext _context;

        public EmployeeService(DbebContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<Employee>> GetAll()
        {
            return await _context.Employees.ToListAsync();
        }

        public async Task<Employee?> GetById(int id)
        {
            return await _context.Employees.FindAsync(id);
        }

        public async Task<Employee> Create(Employee newEmployee)
        {
            _context.Employees.Add(newEmployee);
            await _context.SaveChangesAsync();
            return newEmployee;
        }

        public async Task Update(int id, Employee e)
        {
            var existingEmployed = await GetById(id);
            if (existingEmployed != null)
            {
                existingEmployed.FirstName = e.FirstName;
                await _context.SaveChangesAsync();
            }
        }

        public async Task Delete(Employee e)
        {
            var employeeDelete = await GetById(e.IdEmployee);
            if (employeeDelete != null)
            {
                _context.Employees.Remove(employeeDelete);
                await _context.SaveChangesAsync();
            }
        }

    }



}


*/