﻿using System.Collections;
using DemoProject.Entities;
using DemoProject.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DemoProject.Controllers;

[Route("api/[controller]")]
[ApiController] // [FromBody] ModelState.IsValid
public class CharacterController : ControllerBase
{
    private readonly ICharacterRepository _characterRepository;

    public CharacterController(ICharacterRepository characterRepository)
    {
        _characterRepository = characterRepository;
    }
    
    [HttpGet]
    public IEnumerable<Character> GetAll()
    {
        // var chars= _characterRepository.GetAll();
        // chars.ToList().ForEach(c => c.Nation = null);
        // return chars;
        return _characterRepository.GetAll();
    }

    // api/character/14
    // api/characqter/14
    // api/bla/14
    [HttpGet("{id:int}")]
    public ActionResult<Character?> Get(int id)
    {
        var charretje = _characterRepository.GetAll().SingleOrDefault(x => x.Id == id);
        return charretje == null ? NotFound("Id does not exist") : charretje;
    }
    
    [HttpPost]
    public ActionResult<Character> Post(Character newCharacter)
    {
        _characterRepository.Add(newCharacter);

        // functional GetById()
        var fullChar = _characterRepository.GetAll().Single(x => x.Id == newCharacter.Id);
        return Created("", fullChar); // updated entity
    }
}