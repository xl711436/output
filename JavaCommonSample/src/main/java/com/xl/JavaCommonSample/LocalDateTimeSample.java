package com.xl.JavaCommonSample;

import com.xl.Helper.LocalDateTimeHelper;

import java.time.*;
import java.time.format.DateTimeFormatter;
import java.util.Date;

public class LocalDateTimeSample {
    public static void main(String[] args) {

        LocalDateTimeSample();
        LocalDateTimeConvertSample();
    }


    public static void LocalDateTimeSample()
    {
        String dateString = "2020-10-10 10:10:10";
        DateTimeFormatter format1 = DateTimeFormatter.ofPattern("yyyy-MM-dd HH:mm:ss");
        LocalDateTime dateTime1 = LocalDateTime.parse(dateString,format1);
        LocalDateTime dateTime2 = dateTime1.plusDays(2000);
        LocalDateTime dateTime3 = LocalDateTime.now();
        LocalDateTime dateTime4 = LocalDateTime.of(2015,3,3,20,10,10);
        boolean result =  dateTime1.isAfter(dateTime3);
        System.out.println("result" + result);

        Duration duration = Duration.between(dateTime1,dateTime2);
        long durationSecond = duration.getSeconds();
        System.out.println("durationSecond" + durationSecond);

        int dayYear = dateTime3.getDayOfYear();
        System.out.println("dayYear" + dayYear);

        String strDateTime1 = dateTime1.format(format1);
        String strDateTime2 = dateTime2.format(format1);

        System.out.println("strDateTime1" + strDateTime1);
        System.out.println("strDateTime2" + strDateTime2);


        Instant instant = dateTime4.toInstant(ZoneOffset.ofHours(8));
        long miSecond = instant.toEpochMilli();
        System.out.println("miSecond" + miSecond);

    }


    public static void LocalDateTimeConvertSample()
    {
        LocalDateTime curTime = LocalDateTime.now();

        String tempString = LocalDateTimeHelper.DateToString(curTime);
        LocalDateTime tempDateTime1 = LocalDateTimeHelper.StringToDateTime(tempString);

        Date tempDate = LocalDateTimeHelper.LocalDateTimeToDate(tempDateTime1);
        LocalDateTime tempDateTime2 = LocalDateTimeHelper.DateToLocalDateTime(tempDate);

        Instant tempInstant =  LocalDateTimeHelper.LocalDateTimeToInstant(tempDateTime2);
        LocalDateTime tempDateTime3 = LocalDateTimeHelper.InstantToLocalDateTime(tempInstant);

        long tempMiSecond = LocalDateTimeHelper.LocalDateTimeToMiSecond(tempDateTime3);
        LocalDateTime tempDateTime4 = LocalDateTimeHelper.MiSecondToLocalDateTime(tempMiSecond);

        boolean result= tempDateTime1.isEqual(tempDateTime4);
        System.out.println("result" + result);

    }

}
