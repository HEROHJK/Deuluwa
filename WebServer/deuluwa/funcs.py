import datetime as dt


def getEndTime(startTimeStr, courseTime):
    startTime = dt.datetime.strptime(startTimeStr[:2] + ':' + startTimeStr[2:], '%H:%M')

    endTime = startTime + dt.timedelta(minutes=courseTime)

    return (startTime,endTime)