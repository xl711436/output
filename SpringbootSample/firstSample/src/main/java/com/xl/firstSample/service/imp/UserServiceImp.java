package com.xl.firstSample.service.imp;

import com.xl.firstSample.service.IUserService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.data.mongodb.core.MongoTemplate;
import org.springframework.data.mongodb.core.query.Criteria;
import org.springframework.data.mongodb.core.query.Query;
import org.springframework.data.redis.core.RedisTemplate;
import org.springframework.jdbc.core.JdbcTemplate;
import org.springframework.stereotype.Service;

import java.util.List;
import java.util.Map;

@Service
public class UserServiceImp implements IUserService {

    @Autowired
    private JdbcTemplate jdbcTemplate;

    @Autowired
    private MongoTemplate mongoTemplate;

    @Autowired
    private RedisTemplate<String,  String> redisTemplate;

    @Override
    public List<Map<String,Object>> getUserName(){
        List<Map<String,Object>> result =jdbcTemplate.queryForList("select * from student");
        return result;
    }

    @Override
    public String queryMongodb()
    {
        Query query = new Query(Criteria.where("id").is(1));
        // 查询一条满足条件的数据
        Map result = mongoTemplate.findOne(query, Map.class, "testtable");
        return result.toString();
    }

    @Override
    public String  redisTest()
    {
        redisTemplate.opsForValue().set("aa","bb");
        String getValue  = redisTemplate.opsForValue().get("aa");
        return getValue;
    }

}
