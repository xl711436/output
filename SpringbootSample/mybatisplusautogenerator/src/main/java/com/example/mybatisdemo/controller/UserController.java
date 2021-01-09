package com.example.mybatisdemo.controller;


import com.baomidou.mybatisplus.core.conditions.query.QueryWrapper;
import com.baomidou.mybatisplus.core.conditions.update.LambdaUpdateWrapper;
import com.baomidou.mybatisplus.core.conditions.update.UpdateWrapper;
import com.baomidou.mybatisplus.core.metadata.IPage;
import com.baomidou.mybatisplus.extension.plugins.pagination.Page;
import com.example.mybatisdemo.entity.User;
import com.example.mybatisdemo.service.IUserService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.RequestMapping;

import org.springframework.web.bind.annotation.RestController;

import java.util.List;

/**
 * <p>
 *  前端控制器
 * </p>
 *
 * @author jobob
 * @since 2021-01-09
 */
@RestController
@RequestMapping("/user")
public class UserController {
    @Autowired
    private IUserService userService;

    @RequestMapping("/serviceTest")
    public int serviceTest(){
        User inserUser = new User();
        inserUser.setId(310L);
        inserUser.setAge(100);
        inserUser.setName("bbsad");
        inserUser.setEmail("aa@bb.com");
        userService.save (inserUser);

        User updateUser = new User();
        updateUser.setId(10L);
        updateUser.setAge(1000);
        userService.updateById(updateUser);

        UpdateWrapper<User> updateWrapper = new UpdateWrapper<>();
        updateWrapper.eq("age", "1000");
        User user = new User();
        user.setAge(18);
        boolean rows = userService.update(user, updateWrapper);

        UpdateWrapper<User> updateWrapper1 = new UpdateWrapper<>();
        updateWrapper1.eq("age", 18).set("age", 35);
        rows = userService.update(null, updateWrapper1);

        //lamada 方式update
        LambdaUpdateWrapper<User> lambdaUpdateWrapper = new LambdaUpdateWrapper<>();
        lambdaUpdateWrapper.eq(User::getAge, 35).set(User::getAge, 34);
        rows = userService.update(null, lambdaUpdateWrapper);

        QueryWrapper<User> deleteQw = new QueryWrapper<User>();
        deleteQw.between("age",30,35);
        userService.remove(deleteQw);

        return 1;
    }

    @RequestMapping("/pageTest")
    public int pageTest()
    {
        QueryWrapper<User> ew = new QueryWrapper<User>();
        ew.orderByDesc("id");
        ew.between("age",20,30);

        IPage<User> firstPage = new Page<>(1, 2);//参数一是当前页，参数二是每页个数
        IPage<User> secondPage = new Page<>(2, 2);//参数一是当前页，参数二是每页个数

        IPage<User> curPage = userService.page(new Page<>(1, 2), ew);
        long pages = curPage.getPages();


        List<User> firstList = userService.page(firstPage, ew).getRecords();
        List<User> secondList = userService.page(secondPage, ew).getRecords();

        return 2;
    }

}
