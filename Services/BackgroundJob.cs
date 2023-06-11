namespace DataAPI.Services;

public class BackgroundJob : BackgroundService{
    public BackgroundJob(){
        
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        throw new NotImplementedException();
    }

    protected Task RunChecks(){
     return null;
    }

}