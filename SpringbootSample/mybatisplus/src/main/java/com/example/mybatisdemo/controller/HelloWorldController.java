package com.example.mybatisdemo.controller;

import com.baomidou.mybatisplus.core.conditions.query.QueryWrapper;
import com.baomidou.mybatisplus.core.conditions.update.LambdaUpdateWrapper;
import com.baomidou.mybatisplus.core.conditions.update.UpdateWrapper;
import com.baomidou.mybatisplus.core.metadata.IPage;
import com.baomidou.mybatisplus.extension.plugins.pagination.Page;
import com.example.mybatisdemo.bean.User;
import com.example.mybatisdemo.mapper.UserMapper;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

import java.util.List;

@RestController
@RequestMapping("/hello")
public class HelloWorldController {

    @Autowired
    private UserMapper userMapper;


    @RequestMapping("/say")
    public String say(){
        return "Hello World";
    }

    @RequestMapping("/plus")
    public List<User> plus(){
      List<User> curList =    userMapper.selectList(null);
      return curList;
    }

    @RequestMapping("/plusxml")
    public int plusxml(){
        return userMapper.getCount();
    }

    @RequestMapping("/getAllByXml")
    public List<User>  getAllByXml(){
        return userMapper.getAllByXml();
    }

    @RequestMapping("/mapperTest")
    public int mapperTest() {

        User inserUser = new User();
        inserUser.setId(80L);
        inserUser.setAge(100);
        inserUser.setName("bbsad");
        inserUser.setEmail("aa@bb.com");
        userMapper.insert(inserUser);

        User updateUser = new User();
        updateUser.setId(10L);
        updateUser.setAge(1000);
        userMapper.updateById(updateUser);

        UpdateWrapper<User> updateWrapper = new UpdateWrapper<>();
        updateWrapper.eq("age", "1000");
        User user = new User();
        user.setAge(18);
        Integer rows = userMapper.update(user, updateWrapper);

        UpdateWrapper<User> updateWrapper1 = new UpdateWrapper<>();
        updateWrapper1.eq("age", 18).set("age", 35);
        rows = userMapper.update(null, updateWrapper1);

        //lamada 方式update
        LambdaUpdateWrapper<User> lambdaUpdateWrapper = new LambdaUpdateWrapper<>();
        lambdaUpdateWrapper.eq(User::getAge, 35).set(User::getAge, 34);
        rows = userMapper.update(null, lambdaUpdateWrapper);

        QueryWrapper<User> deleteQw = new QueryWrapper<User>();
        deleteQw.between("age",30,35);
        userMapper.delete(deleteQw);

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

        IPage<User> curPage = userMapper.selectPage(new Page<>(1, 2), ew);
        long pages = curPage.getPages();


        List<User> firstList = userMapper.selectPage(firstPage, ew).getRecords();
        List<User> secondList = userMapper.selectPage(secondPage, ew).getRecords();

        return 2;
    }
}
