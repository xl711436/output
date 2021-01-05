package com.example.mybatisdemo.mapper;


import com.example.mybatisdemo.bean.Student;
import org.apache.ibatis.annotations.Mapper;

import java.util.List;

@Mapper
public interface StudentMapper {
    int getCount();
    List<Student> getAll();
}
