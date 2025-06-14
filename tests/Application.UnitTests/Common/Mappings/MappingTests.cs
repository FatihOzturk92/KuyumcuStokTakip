﻿using System.Reflection;
using System.Runtime.CompilerServices;
using AutoMapper;
using KuyumcuStokTakip.Application.Common.Interfaces;
using KuyumcuStokTakip.Application.Common.Models;
using KuyumcuStokTakip.Application.TodoItems.Queries.GetTodoItemsWithPagination;
using KuyumcuStokTakip.Application.TodoLists.Queries.GetTodos;
using KuyumcuStokTakip.Application.InventoryProducts.Queries.GetInventoryProducts;
using KuyumcuStokTakip.Application.Expenses.Queries.GetExpenses;
using KuyumcuStokTakip.Application.Partners.Queries.GetPartners;
using KuyumcuStokTakip.Domain.Entities;
using KuyumcuStokTakip.Domain.Entities.Account;
using KuyumcuStokTakip.Domain.Entities.Inventory;
using KuyumcuStokTakip.Domain.Entities.Finance;
using NUnit.Framework;

namespace KuyumcuStokTakip.Application.UnitTests.Common.Mappings;

public class MappingTests
{
    private readonly IConfigurationProvider _configuration;
    private readonly IMapper _mapper;

    public MappingTests()
    {
        _configuration = new MapperConfiguration(config => 
            config.AddMaps(Assembly.GetAssembly(typeof(IApplicationDbContext))));

        _mapper = _configuration.CreateMapper();
    }

    [Test]
    public void ShouldHaveValidConfiguration()
    {
        _configuration.AssertConfigurationIsValid();
    }

    [Test]
    [TestCase(typeof(TodoList), typeof(TodoListDto))]
    [TestCase(typeof(TodoItem), typeof(TodoItemDto))]
    [TestCase(typeof(TodoList), typeof(LookupDto))]
    [TestCase(typeof(TodoItem), typeof(LookupDto))]
    [TestCase(typeof(TodoItem), typeof(TodoItemBriefDto))]
    [TestCase(typeof(InventoryProduct), typeof(InventoryProductDto))]
    [TestCase(typeof(Expense), typeof(ExpenseDto))]
    [TestCase(typeof(Partner), typeof(PartnerDto))]
    public void ShouldSupportMappingFromSourceToDestination(Type source, Type destination)
    {
        var instance = GetInstanceOf(source);

        _mapper.Map(instance, source, destination);
    }

    private object GetInstanceOf(Type type)
    {
        if (type.GetConstructor(Type.EmptyTypes) != null)
            return Activator.CreateInstance(type)!;

        // Type without parameterless constructor
        return RuntimeHelpers.GetUninitializedObject(type);
    }
}
