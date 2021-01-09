package com.example.mybatisdemo.mapper;

import com.baomidou.mybatisplus.core.mapper.BaseMapper;
import com.example.mybatisdemo.bean.User;

import java.util.List;

public interface UserMapper extends BaseMapper<User> {
  int getCount();
  List<User> getAllByXml();
}
