package com.xl.Helper;

import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.time.*;
import java.time.format.DateTimeFormatter;
import java.util.Date;

// 时间相关的帮助类, LocalDateTime String  Instance  MiSecond 的互相转换
public class LocalDateTimeHelper {
    public static String DateToString(LocalDateTime I_Date)
    {
        DateTimeFormatter format1 = DateTimeFormatter.ofPattern("yyyy-MM-dd HH:mm:ss");
        String R_Result = I_Date.format(format1);
        return R_Result;
    }

    public static LocalDateTime StringToDateTime(String I_DateString)
    {
        DateTimeFormatter format1 = DateTimeFormatter.ofPattern("yyyy-MM-dd HH:mm:ss");
        LocalDateTime R_Result = LocalDateTime.parse(I_DateString,format1);
        return R_Result;
    }

    public static LocalDateTime DateToLocalDateTime(Date I_Date)
    {
        Instant instant = I_Date.toInstant();
        ZoneId zoneId = ZoneId.systemDefault();
        LocalDateTime localDateTime = instant.atZone(zoneId).toLocalDateTime();
        return localDateTime;
    }

    public static Date LocalDateTimeToDate(LocalDateTime I_LocalDateTime)
    {
        Date date = Date.from(LocalDateTimeToInstant(I_LocalDateTime));
        return date;
    }

    public static Instant LocalDateTimeToInstant(LocalDateTime I_LocalDateTime)
    {
        ZoneId zoneId = ZoneId.systemDefault();
        ZonedDateTime zdt = I_LocalDateTime.atZone(zoneId);
        Instant instant =zdt.toInstant();
        return instant;
    }

    public static LocalDateTime InstantToLocalDateTime(Instant I_Instant)
    {
        ZoneId zoneId = ZoneId.systemDefault();
        LocalDateTime localDateTime = LocalDateTime.ofInstant(I_Instant,zoneId);
        return localDateTime;
    }

    public static LocalDateTime MiSecondToLocalDateTime(long I_MiSecond)
    {
        Instant ins = Instant.ofEpochMilli(I_MiSecond);
        return InstantToLocalDateTime(ins);
    }

    public static long LocalDateTimeToMiSecond(LocalDateTime I_LocalDateTime)
    {
        Instant ins =LocalDateTimeToInstant(I_LocalDateTime);
        return ins.toEpochMilli();
    }

}
