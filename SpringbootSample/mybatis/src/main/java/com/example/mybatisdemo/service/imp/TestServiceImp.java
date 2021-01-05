package com.example.mybatisdemo.service.imp;

import com.example.mybatisdemo.bean.Student;
import com.example.mybatisdemo.mapper.StudentMapper;
import com.example.mybatisdemo.service.TestService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.List;

@Service
public class TestServiceImp implements TestService {
    @Autowired
    private StudentMapper studentMapper;

    @Override
    public int  getTestCount( ) {
        return  studentMapper.getCount();
    }
    @Override
    public List<Student> getAll()
    {
        return studentMapper.getAll();
    }
}
