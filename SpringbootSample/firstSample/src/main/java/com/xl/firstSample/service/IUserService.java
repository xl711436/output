package com.xl.firstSample.service;

import java.util.List;
import java.util.Map;

public interface IUserService {

    List<Map<String,Object>> getUserName();

    String queryMongodb();

    String redisTest();
}
