using Newtonsoft.Json;
using PersonalFinanceManagement.Data.IRepositories;
using PersonalFinanceManagement.Domain.Commons;
using PersonalFinanceManagement.Domain.Configurations;
using PersonalFinanceManagement.Domain.Entities;
using System.Data;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : Auditable
{
    private readonly string path;
    public Repository()
    {
        if (typeof(User) == typeof(TEntity))
            path = Database.USER_PATH;
        else if(typeof(Income) == typeof(TEntity))
            path = Database.INCOME_PATH;
        else if (typeof(Expense) == typeof(TEntity))
            path = Database.EXPENSE_PATH;
        else if (typeof(Budget) == typeof(TEntity)) 
            path = Database.BUDGET_PATH;
    }
    public async Task<bool> DeleteByIdAsync(int id)
    {
        bool isUserAvailable = false;
        var entitiesForWrite = new List<TEntity>();
        var entities = await this.SelectAllAsync();
        foreach (var entity in entities)
        {
            if (entity.Id == id)
            {
                isUserAvailable = true;
                continue;
            }
            entitiesForWrite.Add(entity);

        }
        await WriteToFileAcync(entitiesForWrite);
        return isUserAvailable;
    }

    public async Task<bool> InsertAsync(TEntity entity)
    {
        entity.Id = await GenerateIdAsync();
        entity.CreatedAt = DateTime.Now;
        var entities = await SelectAllAsync();
        entities.Add(entity);
        await this.WriteToFileAcync(entities);
        return true;
    }

    public async Task<List<TEntity>> SelectAllAsync()
    {

        var model = await File.ReadAllTextAsync(path);
        if (string.IsNullOrEmpty(model))
        {
            model = "[]";
        }
        var result = JsonConvert.DeserializeObject<List<TEntity>>(model);
        return result;
    }

    public async Task<TEntity> SelectByIdAsync(int id)
    {
        var entities = await this.SelectAllAsync();
        var entity = entities.Where(e => e.Id == id).FirstOrDefault();
        return entity;
    }

    public async Task<bool> UpdateAsync(TEntity entity)
    {
        bool isUserAvailable = false;
        var entitisForWrite = new List<TEntity>();
        var entities = await this.SelectAllAsync();
        foreach (var person in entities)
        {
            if (person.Id == entity.Id)
            {
                entitisForWrite.Add(entity);
                isUserAvailable = true;
            }
            entitisForWrite.Add(person);
        }
        await WriteToFileAcync(entitisForWrite);
        return isUserAvailable;
    }

    private async Task<int> GenerateIdAsync()
    {
        var users = await SelectAllAsync();
        if (users.Count() == 0)
            return 1;

        var lastId = users.Max(x => x.Id);
        return ++lastId;
    }



    private async Task WriteToFileAcync(List<TEntity> entities)
    {
        var str = JsonConvert.SerializeObject(entities, Formatting.Indented);
        await File.WriteAllTextAsync(path, str);
    }
}
