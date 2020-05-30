﻿using DevOpsMetrics.Core;
using DevOpsMetrics.Service.DataAccess.TableStorage;
using DevOpsMetrics.Service.Models.AzureDevOps;
using DevOpsMetrics.Service.Models.Common;
using DevOpsMetrics.Service.Models.GitHub;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DevOpsMetrics.Service.DataAccess
{
    public class ChangeFailureRateDA
    {
        public async Task<ChangeFailureRateModel> GetChangeFailureRate(bool getSampleData, TableStorageAuth tableStorageAuth,
                bool isAzureDevOps, string organization_owner, string project_repo, string branch, string buildName_workflowName, string buildId_workflowId,
                int numberOfDays, int maxNumberOfItems, bool useCache)
        {
            ListUtility<Build> utility = new ListUtility<Build>();
            ChangeFailureRate changeFailureRate = new ChangeFailureRate();
            if (getSampleData == false)
            {
                              //Gets a list of change failure rate builds
                AzureTableStorageDA daTableStorage = new AzureTableStorageDA();
                Newtonsoft.Json.Linq.JArray list = daTableStorage.GetTableStorageItems(tableStorageAuth, tableStorageAuth.TableChangeFailureRate, daTableStorage.CreateAzureDevOpsBuildPartitionKey(organization_owner, project_repo, buildId_workflowId));
                List<ChangeFailureRateBuild> builds = JsonConvert.DeserializeObject<List<ChangeFailureRateBuild>>(list.ToString());
                List<KeyValuePair<DateTime, bool>> dateList = new List<KeyValuePair<DateTime, bool>>();

                //Build the date list and then generate the change failure rate metric
                foreach (ChangeFailureRateBuild item in builds)
                {
                    KeyValuePair<DateTime, bool> newItem = new KeyValuePair<DateTime, bool>(item.StartTime, item.DeploymentWasSuccessful);
                    dateList.Add(newItem);
                }
                float changeFailureRateMetric = changeFailureRate.ProcessChangeFailureRate(dateList, "", numberOfDays);

                ChangeFailureRateModel model = new ChangeFailureRateModel
                {
                    IsAzureDevOps = isAzureDevOps,
                    DeploymentName = buildName_workflowName,                    
                    ChangeFailureRateBuildList = builds,
                    ChangeFailureRateMetric = changeFailureRateMetric,
                    ChangeFailureRateMetricDescription = changeFailureRate.GetChangeFailureRateRating(changeFailureRateMetric)
                };
                return model;
            }
            else
            {
                ChangeFailureRateModel model = new ChangeFailureRateModel
                {
                    IsAzureDevOps = isAzureDevOps,
                    DeploymentName = buildName_workflowName,
                    ChangeFailureRateBuildList = GetSampleBuilds(),
                    ChangeFailureRateMetric = 2f / 10f,
                    ChangeFailureRateMetricDescription = changeFailureRate.GetChangeFailureRateRating(2f / 10f)
                };
                return model;
            }
        }

        private List<ChangeFailureRateBuild> GetSampleBuilds()
        {
            List<ChangeFailureRateBuild> results = new List<ChangeFailureRateBuild>();
            ChangeFailureRateBuild item1 = new ChangeFailureRateBuild
            {
                StartTime = DateTime.Now.AddDays(-7).AddMinutes(-4),
                EndTime = DateTime.Now.AddDays(-7).AddMinutes(0),
                BuildDurationPercent = 70,
                BuildNumber = "1",
                Branch = "master",
                Status = "completed",
                Url = "https://dev.azure.com/samsmithnz/samlearnsazure/1",
                DeploymentWasSuccessful = true
            };
            results.Add(item1);
            results.Add(item1);
            ChangeFailureRateBuild item2 = new ChangeFailureRateBuild
            {
                StartTime = DateTime.Now.AddDays(-5).AddMinutes(-5),
                EndTime = DateTime.Now.AddDays(-5).AddMinutes(0),
                BuildDurationPercent = 40,
                BuildNumber = "2",
                Branch = "master",
                Status = "completed",
                Url = "https://dev.azure.com/samsmithnz/samlearnsazure/2",
                DeploymentWasSuccessful = true
            };
            results.Add(item2);
            results.Add(item2);
            ChangeFailureRateBuild item3 = new ChangeFailureRateBuild
            {
                StartTime = DateTime.Now.AddDays(-4).AddMinutes(-1),
                EndTime = DateTime.Now.AddDays(-4).AddMinutes(0),
                BuildDurationPercent = 20,
                BuildNumber = "3",
                Branch = "master",
                Status = "failed",
                Url = "https://dev.azure.com/samsmithnz/samlearnsazure/3",
                DeploymentWasSuccessful = false
            };
            results.Add(item3);
            ChangeFailureRateBuild item4 = new ChangeFailureRateBuild
            {
                StartTime = DateTime.Now.AddDays(-3).AddMinutes(-4),
                EndTime = DateTime.Now.AddDays(-3).AddMinutes(0),
                BuildDurationPercent = 50,
                BuildNumber = "4",
                Branch = "master",
                Status = "completed",
                Url = "https://dev.azure.com/samsmithnz/samlearnsazure/4",
                DeploymentWasSuccessful = true
            };
            results.Add(item4);
            results.Add(item4);
            ChangeFailureRateBuild item5 = new ChangeFailureRateBuild
            {
                StartTime = DateTime.Now.AddDays(-2).AddMinutes(-7),
                EndTime = DateTime.Now.AddDays(-2).AddMinutes(0),
                BuildDurationPercent = 60,
                BuildNumber = "5",
                Branch = "master",
                Status = "completed",
                Url = "https://dev.azure.com/samsmithnz/samlearnsazure/5",
                DeploymentWasSuccessful = true
            };
            results.Add(item5);
            results.Add(item5);
            ChangeFailureRateBuild item6 = new ChangeFailureRateBuild
            {
                StartTime = DateTime.Now.AddDays(-1).AddMinutes(-5),
                EndTime = DateTime.Now.AddDays(-1).AddMinutes(0),
                BuildDurationPercent = 70,
                BuildNumber = "6",
                Branch = "master",
                Status = "inProgress",
                Url = "https://dev.azure.com/samsmithnz/samlearnsazure/6",
                DeploymentWasSuccessful = false
            };
            results.Add(item6);

            return results;
        }

    }
}
