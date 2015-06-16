//
//  Diffusion.h
//  DiffusionDevelopment
//
//  Created by Christopher Baltzer on 2013-09-12.
//  Copyright (c) 2013 Chris Baltzer. All rights reserved.
//

#import <Foundation/Foundation.h>

void _share(const char *, const char *, const char *, int);
void _prewarm();
void _addCustomActivity(const char *);
bool _isFacebookConnected();
bool _isTwitterConnected();

@interface Diffusion : NSObject

@property (nonatomic, assign) int activityMask;
@property (nonatomic, retain) NSArray* systemActivities;
@property (nonatomic, retain) NSMutableArray* customActivities;


+(Diffusion*)sharedInstance;
-(void)prewarm;
-(void)addCustomActivity:(NSString*)activity;

-(BOOL)isFacebookConnected;
-(BOOL)isTwitterConnected;

-(void)share:(NSString*)message withURL:(NSString*)url andFile:(NSString*)file;

-(void)shareCallback:(NSString*)type didComplete:(BOOL)completed;



-(void)sendMessageToGameObject:(NSString*)gameObject withMethod:(NSString*)method andMessage:(NSString*)message;
-(NSArray*)activiesToHide;
-(int)activityTypeToUnityInt:(NSString*)activity;
@end

