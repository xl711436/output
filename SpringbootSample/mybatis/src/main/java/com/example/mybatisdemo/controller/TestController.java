package com.example.mybatisdemo.controller;

import com.example.mybatisdemo.bean.Student;
import com.example.mybatisdemo.mapper.StudentMapper;
import com.example.mybatisdemo.service.TestService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

import java.util.List;

@RestController
@RequestMapping("/test")
public class TestController {

    @Autowired
    private TestService testService;

    @RequestMapping("/say")
    public String say() {
        return "Hello World";
    }

    @RequestMapping("/getCount")
    public int getCount() {
        return testService.getTestCount();
    }


    @RequestMapping("/getAll")
    public List<Student> getAll() {
        return testService.getAll();
    }
}
