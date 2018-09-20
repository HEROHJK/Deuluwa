import datetime as dt


def getEndTime(startTimeStr, courseTime):
    startTime = dt.datetime.strptime(startTimeStr[:2] + ':' + startTimeStr[2:], '%H:%M')

    endTime = startTime + dt.timedelta(minutes=courseTime)

    return (startTime,endTime)

def getTime(timeStr):
    time = dt.datetime.strptime(timeStr[:4], '%H%M')

    return time

def tardyCheck(startTime, endTime, time):
    if time > endTime:
        return 2
    elif time > startTime:
        return 1
    return 0