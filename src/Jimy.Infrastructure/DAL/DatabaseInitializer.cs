﻿using Jimy.Application.Security;
using Jimy.Core.Entities;
using Jimy.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Jimy.Infrastructure.DAL;

public class DatabaseInitializer : IHostedService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IPasswordManager _passwordManager;

    public DatabaseInitializer(IServiceProvider serviceProvider, IPasswordManager passwordManager)
    {
        _serviceProvider = serviceProvider;
        _passwordManager = passwordManager;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<JimyDbContext>();
        await dbContext.Database.MigrateAsync(cancellationToken);

        if (await dbContext.Exercises.AnyAsync(cancellationToken) || await dbContext.Users.AnyAsync(cancellationToken))
        {
            return;
        }

        var exercises = new List<Exercise>
        {
            new(new ExerciseId(Guid.Parse("00000000-0000-0000-0000-000000000001")), new ExerciseName("Push Up"),
                new ExerciseDescription("A bodyweight exercise for chest.")),
            new(new ExerciseId(Guid.Parse("00000000-0000-0000-0000-000000000002")), new ExerciseName("Pull Up"),
                new ExerciseDescription("An upper-body strength exercise.")),
            new(new ExerciseId(Guid.Parse("00000000-0000-0000-0000-000000000003")), new ExerciseName("Squat"),
                new ExerciseDescription("Targets your lower body and core muscles.")),
            new(new ExerciseId(Guid.Parse("00000000-0000-0000-0000-000000000004")), new ExerciseName("Lunge"),
                new ExerciseDescription("Strengthens your legs and glutes.")),
            new(new ExerciseId(Guid.Parse("00000000-0000-0000-0000-000000000005")), new ExerciseName("Bench Press"),
                new ExerciseDescription("Strength training for the chest.")),
            new(new ExerciseId(Guid.Parse("00000000-0000-0000-0000-000000000006")), new ExerciseName("Deadlift"),
                new ExerciseDescription("A compound lift for the entire body.")),
            new(new ExerciseId(Guid.Parse("00000000-0000-0000-0000-000000000007")),
                new ExerciseName("Overhead Press"), new ExerciseDescription("Shoulder and upper body exercise.")),
            new(new ExerciseId(Guid.Parse("00000000-0000-0000-0000-000000000008")), new ExerciseName("Bicep Curl"),
                new ExerciseDescription("Isolates the bicep muscles.")),
            new(new ExerciseId(Guid.Parse("00000000-0000-0000-0000-000000000009")), new ExerciseName("Tricep Dips"),
                new ExerciseDescription("Strengthens triceps and shoulders.")),
            new(new ExerciseId(Guid.Parse("00000000-0000-0000-0000-000000000010")), new ExerciseName("Plank"),
                new ExerciseDescription("A core stabilization exercise.")),
            new(new ExerciseId(Guid.Parse("00000000-0000-0000-0000-000000000011")),
                new ExerciseName("Mountain Climbers"), new ExerciseDescription("Cardio and core exercise.")),
            new(new ExerciseId(Guid.Parse("00000000-0000-0000-0000-000000000012")), new ExerciseName("Burpees"),
                new ExerciseDescription("Full-body conditioning movement.")),
            new(new ExerciseId(Guid.Parse("00000000-0000-0000-0000-000000000013")),
                new ExerciseName("Jumping Jacks"), new ExerciseDescription("A simple cardio exercise.")),
            new(new ExerciseId(Guid.Parse("00000000-0000-0000-0000-000000000014")),
                new ExerciseName("Russian Twists"), new ExerciseDescription("A rotational core exercise.")),
            new(new ExerciseId(Guid.Parse("00000000-0000-0000-0000-000000000015")), new ExerciseName("Leg Raises"),
                new ExerciseDescription("Targets lower abs.")),
            new(new ExerciseId(Guid.Parse("00000000-0000-0000-0000-000000000016")),
                new ExerciseName("Bicycle Crunches"), new ExerciseDescription("Great for abdominal muscles.")),
            new(new ExerciseId(Guid.Parse("00000000-0000-0000-0000-000000000017")), new ExerciseName("Side Plank"),
                new ExerciseDescription("Strengthens obliques and core.")),
            new(new ExerciseId(Guid.Parse("00000000-0000-0000-0000-000000000018")), new ExerciseName("Box Jumps"),
                new ExerciseDescription("A plyometric exercise for legs.")),
            new(new ExerciseId(Guid.Parse("00000000-0000-0000-0000-000000000019")), new ExerciseName("Jump Squats"),
                new ExerciseDescription("Explosive squat variation for legs.")),
            new(new ExerciseId(Guid.Parse("00000000-0000-0000-0000-000000000020")),
                new ExerciseName("Kettlebell Swings"),
                new ExerciseDescription("Works your glutes, hips, and core.")),
            new(new ExerciseId(Guid.Parse("00000000-0000-0000-0000-000000000021")),
                new ExerciseName("Goblet Squat"),
                new ExerciseDescription("A variation of the squat using weights.")),
            new(new ExerciseId(Guid.Parse("00000000-0000-0000-0000-000000000022")),
                new ExerciseName("Farmers Walk"), new ExerciseDescription("Great for grip and core strength.")),
            new(new ExerciseId(Guid.Parse("00000000-0000-0000-0000-000000000023")), new ExerciseName("Calf Raises"),
                new ExerciseDescription("Targets the calf muscles.")),
            new(new ExerciseId(Guid.Parse("00000000-0000-0000-0000-000000000024")), new ExerciseName("Jump Rope"),
                new ExerciseDescription("A cardio exercise that improves coordination.")),
            new(new ExerciseId(Guid.Parse("00000000-0000-0000-0000-000000000025")),
                new ExerciseName("Battle Ropes"), new ExerciseDescription("Upper body and cardio conditioning.")),
            new(new ExerciseId(Guid.Parse("00000000-0000-0000-0000-000000000026")), new ExerciseName("High Knees"),
                new ExerciseDescription("A dynamic cardio and core exercise.")),
            new(new ExerciseId(Guid.Parse("00000000-0000-0000-0000-000000000027")), new ExerciseName("Step-Ups"),
                new ExerciseDescription("Targets legs, glutes, and balance.")),
            new(new ExerciseId(Guid.Parse("00000000-0000-0000-0000-000000000028")),
                new ExerciseName("Dumbbell Rows"), new ExerciseDescription("Works the back and biceps.")),
            new(new ExerciseId(Guid.Parse("00000000-0000-0000-0000-000000000029")),
                new ExerciseName("Lat Pulldown"), new ExerciseDescription("Strengthens the lats and upper back.")),
            new(new ExerciseId(Guid.Parse("00000000-0000-0000-0000-000000000030")), new ExerciseName("Seated Row"),
                new ExerciseDescription("Works the back, traps, and rear delts.")),
            new(new ExerciseId(Guid.Parse("00000000-0000-0000-0000-000000000031")), new ExerciseName("Hammer Curl"),
                new ExerciseDescription("Targets biceps and forearms.")),
            new(new ExerciseId(Guid.Parse("00000000-0000-0000-0000-000000000032")),
                new ExerciseName("Skull Crushers"), new ExerciseDescription("An effective tricep exercise.")),
            new(new ExerciseId(Guid.Parse("00000000-0000-0000-0000-000000000033")), new ExerciseName("Chest Fly"),
                new ExerciseDescription("Strengthens the chest and shoulders.")),
            new(new ExerciseId(Guid.Parse("00000000-0000-0000-0000-000000000034")),
                new ExerciseName("Dumbbell Press"), new ExerciseDescription("A bench press variation for chest.")),
            new(new ExerciseId(Guid.Parse("00000000-0000-0000-0000-000000000035")),
                new ExerciseName("Incline Press"), new ExerciseDescription("Targets the upper chest.")),
            new(new ExerciseId(Guid.Parse("00000000-0000-0000-0000-000000000036")),
                new ExerciseName("Arnold Press"), new ExerciseDescription("Great for shoulders and arms.")),
            new(new ExerciseId(Guid.Parse("00000000-0000-0000-0000-000000000037")), new ExerciseName("Front Raise"),
                new ExerciseDescription("Isolates the front deltoid muscles.")),
            new(new ExerciseId(Guid.Parse("00000000-0000-0000-0000-000000000038")),
                new ExerciseName("Lateral Raise"), new ExerciseDescription("Strengthens the shoulders.")),
            new(new ExerciseId(Guid.Parse("00000000-0000-0000-0000-000000000039")), new ExerciseName("Reverse Fly"),
                new ExerciseDescription("Targets the rear deltoids and back.")),
            new(new ExerciseId(Guid.Parse("00000000-0000-0000-0000-000000000040")), new ExerciseName("Face Pull"),
                new ExerciseDescription("Improves shoulder and upper back strength.")),
            new(new ExerciseId(Guid.Parse("00000000-0000-0000-0000-000000000041")),
                new ExerciseName("Glute Bridge"), new ExerciseDescription("Targets the glutes and hamstrings.")),
            new(new ExerciseId(Guid.Parse("00000000-0000-0000-0000-000000000042")), new ExerciseName("Hip Thrust"),
                new ExerciseDescription("Strengthens the glutes.")),
            new(new ExerciseId(Guid.Parse("00000000-0000-0000-0000-000000000043")), new ExerciseName("Donkey Kick"),
                new ExerciseDescription("Works the glutes and core.")),
            new(new ExerciseId(Guid.Parse("00000000-0000-0000-0000-000000000044")),
                new ExerciseName("Side Leg Raise"), new ExerciseDescription("Targets the gluteus medius.")),
            new(new ExerciseId(Guid.Parse("00000000-0000-0000-0000-000000000045")), new ExerciseName("Wall Sit"),
                new ExerciseDescription("Strengthens legs and core.")),
            new(new ExerciseId(Guid.Parse("00000000-0000-0000-0000-000000000046")),
                new ExerciseName("Cable Kickback"), new ExerciseDescription("Isolates the glutes.")),
            new(new ExerciseId(Guid.Parse("00000000-0000-0000-0000-000000000047")),
                new ExerciseName("Kettlebell Clean"), new ExerciseDescription("Works the entire body and core.")),
            new(new ExerciseId(Guid.Parse("00000000-0000-0000-0000-000000000048")),
                new ExerciseName("Clean and Jerk"), new ExerciseDescription("A full-body Olympic lift.")),
            new(new ExerciseId(Guid.Parse("00000000-0000-0000-0000-000000000049")), new ExerciseName("Snatch"),
                new ExerciseDescription("A fast, powerful Olympic lift.")),
            new(new ExerciseId(Guid.Parse("00000000-0000-0000-0000-000000000050")),
                new ExerciseName("Sumo Deadlift"), new ExerciseDescription("Targets legs, glutes, and back.")),
            new(new ExerciseId(Guid.Parse("00000000-0000-0000-0000-000000000051")), new ExerciseName("Barbell Row"),
                new ExerciseDescription("Strengthens back and arms.")),
            new(new ExerciseId(Guid.Parse("00000000-0000-0000-0000-000000000052")),
                new ExerciseName("Hex Bar Deadlift"),
                new ExerciseDescription("A safer variation of the deadlift.")),
            new(new ExerciseId(Guid.Parse("00000000-0000-0000-0000-000000000053")),
                new ExerciseName("Bulgarian Split Squat"),
                new ExerciseDescription("A challenging lower body exercise.")),
            new(new ExerciseId(Guid.Parse("00000000-0000-0000-0000-000000000054")),
                new ExerciseName("Pistol Squat"),
                new ExerciseDescription("A single-leg squat for balance and strength.")),
            new(new ExerciseId(Guid.Parse("00000000-0000-0000-0000-000000000055")),
                new ExerciseName("Good Morning"),
                new ExerciseDescription("Strengthens hamstrings and lower back.")),
            new(new ExerciseId(Guid.Parse("00000000-0000-0000-0000-000000000056")),
                new ExerciseName("Glute-Ham Raise"), new ExerciseDescription("Strengthens glutes and hamstrings.")),
            new(new ExerciseId(Guid.Parse("00000000-0000-0000-0000-000000000057")), new ExerciseName("Front Squat"),
                new ExerciseDescription("Targets quads and core.")),
            new(new ExerciseId(Guid.Parse("00000000-0000-0000-0000-000000000058")),
                new ExerciseName("Landmine Press"), new ExerciseDescription("A shoulder press variation.")),
            new(new ExerciseId(Guid.Parse("00000000-0000-0000-0000-000000000059")), new ExerciseName("Floor Press"),
                new ExerciseDescription("A chest and triceps strength exercise.")),
            new(new ExerciseId(Guid.Parse("00000000-0000-0000-0000-000000000060")),
                new ExerciseName("Hollow Body Hold"), new ExerciseDescription("Core stabilization exercise.")),
            new(new ExerciseId(Guid.Parse("00000000-0000-0000-0000-000000000061")),
                new ExerciseName("Reverse Crunch"), new ExerciseDescription("Targets lower abs.")),
            new(new ExerciseId(Guid.Parse("00000000-0000-0000-0000-000000000062")),
                new ExerciseName("Windshield Wipers"), new ExerciseDescription("A rotational core exercise.")),
            new(new ExerciseId(Guid.Parse("00000000-0000-0000-0000-000000000063")), new ExerciseName("Bear Crawl"),
                new ExerciseDescription("A full-body conditioning movement.")),
            new(new ExerciseId(Guid.Parse("00000000-0000-0000-0000-000000000064")), new ExerciseName("Crab Walk"),
                new ExerciseDescription("Strengthens arms and core.")),
            new(new ExerciseId(Guid.Parse("00000000-0000-0000-0000-000000000065")), new ExerciseName("Inchworm"),
                new ExerciseDescription("A full-body mobility and core exercise.")),
            new(new ExerciseId(Guid.Parse("00000000-0000-0000-0000-000000000066")),
                new ExerciseName("Turkish Get-Up"),
                new ExerciseDescription("A full-body strength and mobility exercise.")),
            new(new ExerciseId(Guid.Parse("00000000-0000-0000-0000-000000000067")),
                new ExerciseName("Clapping Push-Ups"), new ExerciseDescription("An explosive push-up variation.")),
            new(new ExerciseId(Guid.Parse("00000000-0000-0000-0000-000000000068")),
                new ExerciseName("Diamond Push-Ups"), new ExerciseDescription("Targets triceps and chest.")),
            new(new ExerciseId(Guid.Parse("00000000-0000-0000-0000-000000000069")),
                new ExerciseName("Decline Push-Ups"),
                new ExerciseDescription("Targets upper chest and shoulders.")),
            new(new ExerciseId(Guid.Parse("00000000-0000-0000-0000-000000000070")), new ExerciseName("Superman"),
                new ExerciseDescription("Strengthens lower back and glutes.")),
            new(new ExerciseId(Guid.Parse("00000000-0000-0000-0000-000000000071")),
                new ExerciseName("Reverse Lunge"), new ExerciseDescription("Targets glutes and quads.")),
            new(new ExerciseId(Guid.Parse("00000000-0000-0000-0000-000000000072")), new ExerciseName("Side Lunge"),
                new ExerciseDescription("Strengthens legs and increases mobility.")),
            new(new ExerciseId(Guid.Parse("00000000-0000-0000-0000-000000000073")),
                new ExerciseName("Weighted Step-Up"),
                new ExerciseDescription("Improves leg strength and balance.")),
            new(new ExerciseId(Guid.Parse("00000000-0000-0000-0000-000000000074")), new ExerciseName("Incline Row"),
                new ExerciseDescription("Strengthens back and shoulders.")),
            new(new ExerciseId(Guid.Parse("00000000-0000-0000-0000-000000000075")),
                new ExerciseName("Bodyweight Row"), new ExerciseDescription("A beginner back and arms exercise.")),
            new(new ExerciseId(Guid.Parse("00000000-0000-0000-0000-000000000076")), new ExerciseName("Jump Lunge"),
                new ExerciseDescription("Explosive lunge variation for legs.")),
            new(new ExerciseId(Guid.Parse("00000000-0000-0000-0000-000000000077")),
                new ExerciseName("Weighted Squat Jump"),
                new ExerciseDescription("Targets explosive power in the legs.")),
            new(new ExerciseId(Guid.Parse("00000000-0000-0000-0000-000000000078")), new ExerciseName("Sled Push"),
                new ExerciseDescription("Strengthens legs, core, and conditioning.")),
            new(new ExerciseId(Guid.Parse("00000000-0000-0000-0000-000000000079")), new ExerciseName("Sled Pull"),
                new ExerciseDescription("Targets back, legs, and cardio endurance.")),
            new(new ExerciseId(Guid.Parse("00000000-0000-0000-0000-000000000080")), new ExerciseName("Split Squat"),
                new ExerciseDescription("A great unilateral leg exercise.")),
            new(new ExerciseId(Guid.Parse("00000000-0000-0000-0000-000000000081")),
                new ExerciseName("Isometric Hold"), new ExerciseDescription("Core and strength stabilization.")),
            new(new ExerciseId(Guid.Parse("00000000-0000-0000-0000-000000000082")),
                new ExerciseName("Zercher Squat"),
                new ExerciseDescription("A squat variation for strength and mobility.")),
            new(new ExerciseId(Guid.Parse("00000000-0000-0000-0000-000000000083")),
                new ExerciseName("Band Pull Apart"), new ExerciseDescription("Strengthens shoulders and back.")),
            new(new ExerciseId(Guid.Parse("00000000-0000-0000-0000-000000000084")),
                new ExerciseName("Landmine Squat"), new ExerciseDescription("A lower body strength exercise.")),
            new(new ExerciseId(Guid.Parse("00000000-0000-0000-0000-000000000085")),
                new ExerciseName("Landmine Deadlift"),
                new ExerciseDescription("A deadlift variation using the landmine.")),
            new(new ExerciseId(Guid.Parse("00000000-0000-0000-0000-000000000086")), new ExerciseName("Sled Row"),
                new ExerciseDescription("Strengthens back and cardio conditioning.")),
            new(new ExerciseId(Guid.Parse("00000000-0000-0000-0000-000000000087")),
                new ExerciseName("Single Arm Row"), new ExerciseDescription("Strengthens the back and arms.")),
            new(new ExerciseId(Guid.Parse("00000000-0000-0000-0000-000000000088")), new ExerciseName("Band Row"),
                new ExerciseDescription("Strengthens back and shoulders.")),
            new(new ExerciseId(Guid.Parse("00000000-0000-0000-0000-000000000089")),
                new ExerciseName("Weighted Sit-Up"), new ExerciseDescription("A core exercise with resistance.")),
            new(new ExerciseId(Guid.Parse("00000000-0000-0000-0000-000000000090")),
                new ExerciseName("Russian Kettlebell Swing"),
                new ExerciseDescription("A kettlebell exercise for legs and core.")),
            new(new ExerciseId(Guid.Parse("00000000-0000-0000-0000-000000000091")),
                new ExerciseName("Suitcase Carry"),
                new ExerciseDescription("A unilateral carry for core and grip strength.")),
            new(new ExerciseId(Guid.Parse("00000000-0000-0000-0000-000000000092")),
                new ExerciseName("Bear Hug Carry"),
                new ExerciseDescription("A loaded carry for strength and conditioning.")),
            new(new ExerciseId(Guid.Parse("00000000-0000-0000-0000-000000000093")),
                new ExerciseName("Overhead Carry"), new ExerciseDescription("A full-body stabilization exercise.")),
            new(new ExerciseId(Guid.Parse("00000000-0000-0000-0000-000000000094")), new ExerciseName("Tire Flip"),
                new ExerciseDescription("An explosive full-body exercise.")),
            new(new ExerciseId(Guid.Parse("00000000-0000-0000-0000-000000000095")), new ExerciseName("Broad Jump"),
                new ExerciseDescription("A plyometric lower body exercise.")),
            new(new ExerciseId(Guid.Parse("00000000-0000-0000-0000-000000000096")),
                new ExerciseName("Overhead Squat"),
                new ExerciseDescription("A squat variation for core and shoulder stability.")),
            new(new ExerciseId(Guid.Parse("00000000-0000-0000-0000-000000000097")), new ExerciseName("Wall Ball"),
                new ExerciseDescription("A conditioning exercise using a medicine ball.")),
            new(new ExerciseId(Guid.Parse("00000000-0000-0000-0000-000000000098")),
                new ExerciseName("Weighted Russian Twist"),
                new ExerciseDescription("A weighted core exercise for obliques.")),
            new(new ExerciseId(Guid.Parse("00000000-0000-0000-0000-000000000099")), new ExerciseName("Man Maker"),
                new ExerciseDescription("A full-body conditioning movement.")),
            new(new ExerciseId(Guid.Parse("00000000-0000-0000-0000-000000000100")), new ExerciseName("Thruster"),
                new ExerciseDescription("A combination of a squat and overhead press."))
        };

        var adminUser = new User(
            new UserId(Guid.Parse("00000000-0000-0000-0000-000000000001")),
            new Email("admin@jimy.io"),
            new Username("admin"),
            _passwordManager.Secure("admin123"),
            Role.Admin(),
            DateTime.UtcNow
        );

        await dbContext.Exercises.AddRangeAsync(exercises, cancellationToken);
        await dbContext.Users.AddAsync(adminUser, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
