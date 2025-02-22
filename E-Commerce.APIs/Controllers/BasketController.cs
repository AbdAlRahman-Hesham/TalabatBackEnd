﻿using E_Commerce.Domain.Entities;
using E_Commerce.DTOs.BasketDTOs;
using E_Commerce.DTOs.ErrorResponse;
using E_Commerce.Repository.Reprositories_Interfaces;
using Microsoft.AspNetCore.Mvc;
using Mapster;


namespace E_Commerce.APIs.Controllers;


public class BasketController(IBasketRepository basketRepository) : BaseApiController
{
    private readonly IBasketRepository _basketRepository = basketRepository;

    [HttpGet]
    public async Task<ActionResult<UserBasket>> GetBasket(string id)
    {
        var basket = await _basketRepository.GetBasketAsync(id);
        return Ok(basket ?? new UserBasket(id));
    }
    [HttpPost]
    public async Task<ActionResult<UserBasket>> UpdateBasket(UserBasketDto userBasketDto)
    {
        var userBasket = userBasketDto.Adapt<UserBasket>();
        var basket = await _basketRepository.UpdateBasketAsync(userBasket);
        if (basket == null) return BadRequest(new ApiResponse(400,"Problem updating the basket"));
        return Ok(basket);
    }
    [HttpDelete]
    public async Task<ActionResult<bool>> DeleteBasket(string id)
    {
        var result = await _basketRepository.DeleteBasketAsync(id);
        return Ok(result);
    }

     
}
