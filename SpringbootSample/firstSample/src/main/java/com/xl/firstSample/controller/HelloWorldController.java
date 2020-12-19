package com.xl.firstSample.controller;

import com.xl.firstSample.service.IUserService;
import com.xl.firstSample.service.imp.UserServiceImp;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

import java.util.List;
import java.util.Map;


@RestController
    @RequestMapping("/hello")
    public class HelloWorldController {

    @Autowired
    IUserService service;

    @RequestMapping("/say")
    public String say() {
        return "Hello World";
    }

    @RequestMapping("/queryUserName")
    public List<Map<String, Object>> queryUserName() {
        return service.getUserName();
    }

    @RequestMapping("/queryMongodb")
    public String queryMongodb() {
        return service.queryMongodb();
    }
}


