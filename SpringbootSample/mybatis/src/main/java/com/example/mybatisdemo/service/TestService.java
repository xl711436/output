package com.example.mybatisdemo.service;


import com.example.mybatisdemo.bean.Student;

import java.util.List;

public interface TestService {
    int getTestCount();

    List<Student> getAll();
}
