[![.NET](https://github.com/aimenux/SerilogSinksDemo/actions/workflows/ci.yml/badge.svg?branch=main)](https://github.com/aimenux/SerilogSinksDemo/actions/workflows/ci.yml)

# SerilogSinksDemo
```
Using Serilog to send logs to various sinks
```

> In this repo, i m using serilog in order to enable logging to various sinks in console applications :
>
> - 2 configuration ways : code config based or json config based (chosen randomly at startup)
>
> - 5 sinks : console, file, udp, seq, appinsights
>
> For Seq: 
> - run docker command : `docker run -e "ACCEPT_EULA=Y" -p 5341:80 --name=SEQ -d datalust/seq:latest`
> - navigate to `http://localhost:5341` to see logs 
> 
> ![SerilogSinksDemo](Screenshots/SerilogSinksDemo.png)
>

**`Tools`** : net 8.0, serilog