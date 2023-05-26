﻿namespace DevOpsMetrics.Core.Models.Common
{
    public class DORASummaryItem
    {
        public string Owner
        {
            get; set;
        }
        public string Repo
        {
            get; set;
        }
        public float DeploymentFrequency
        {
            get; set;
        }
        public string DeploymentFrequencyBadgeURL
        {
            get; set;
        }
        public string DeploymentFrequencyBadgeWithMetricURL
        {
            get; set;
        }
        public float LeadTimeForChanges
        {
            get; set;
        }
        public string LeadTimeForChangesBadgeURL
        {
            get; set;
        }
        public string LeadTimeForChangesBadgeWithMetricURL
        {
            get; set;
        }
        public float MeanTimeToRestore
        {
            get; set;
        }
        public string MeanTimeToRestoreBadgeURL
        {
            get; set;
        }
        public string MeanTimeToRestoreBadgeWithMetricURL
        {
            get; set;
        }
        public float ChangeFailureRate
        {
            get; set;
        }
        public string ChangeFailureRateBadgeURL
        {
            get; set;
        }
        public string ChangeFailureRateBadgeWithMetricURL
        {
            get; set;
        }
        public string ProcessingLogMessage
        {
            get; set;
        }
    }
}
