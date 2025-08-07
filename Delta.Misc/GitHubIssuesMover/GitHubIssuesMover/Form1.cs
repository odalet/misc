using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Windows.Forms;
using Octokit;
using O = Octokit;

namespace GitHubIssuesMover
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private GitHubClient client;

        private async void button1_Click(object sender, EventArgs e)
        {
            //var credentials = new Credentials("odalet", "sopra*");
            //var store = new 
            //var conn = new Connection("GitHubIssuesMover");
            client = new GitHubClient(new ProductHeaderValue("GitHubIssuesMover"));
            client.Credentials = new Credentials("odalet", "sopra*");

            //var user = await client.User.Get("odalet");
            //MessageBox.Show("email = " + user.Email);

            Cursor = Cursors.WaitCursor;

            var issues1 = await client.Issue.GetForRepository("odalet", "Delta", new RepositoryIssueRequest() { State = ItemState.Open });
            var issues2 = await client.Issue.GetForRepository("odalet", "Delta", new RepositoryIssueRequest() { State = ItemState.Closed });
            var issues = issues1.Union(issues2);

            Cursor = Cursors.Default;

            var list = new List<O.Label>();
            foreach (var issue in issues)
                list.AddRange(issue.Labels.Select(l => l));
            var labels = list.Distinct().OrderBy(l => l.Name);

            Log(string.Join(", ", labels.Select(l => l.Name).ToArray()) + "\r\n");
            Log("-----------------------------------------------------------\r\n");
            var certxIssues = issues.Select(iss => iss.Labels.Select(l => l.Name).Contains("CertXplorer"));


            foreach (var issue in issues)
            {
                var s = "Issue #" + issue.Number + " --> " + issue.Title + " [" + issue.State + "]";
                var ll = string.Join(", ", issue.Labels.Select(l => l.Name).ToArray());
                s += "\t" + ll + "\r\n";

                Log(s);
            }

            Log("********************************************************************************\r\n");

            //// Let's get the  WSQ issue
            //var wsqIssue = issues.Single(i => i.Number == 15);
            ////wsqIssue.Labels.Remove(labels.Single(l => l.Name == "WSQ"));
            ////wsqIssue.Title = "Can't open a WSQ file with no resolution information";
            //wsqIssue.Body = "_Previously issue #15 in Delta repository_\r\n\r\n" + wsqIssue.Body;
            //DumpIssue(wsqIssue);

            //try
            //{
            //    var newWsqIssue = new NewIssue(wsqIssue.Title);
            //    newWsqIssue.Body = wsqIssue.Body;
            //    newWsqIssue.Assignee = wsqIssue.Assignee == null ? null : wsqIssue.Assignee.Login;
            //    foreach (var l in wsqIssue.Labels)
            //        newWsqIssue.Labels.Add(l.Name);
            //    newWsqIssue.Milestone = wsqIssue.Milestone == null ? null : (int?)wsqIssue.Milestone.Number;
            //    var migrated = await client.Issue.Create("odalet", "Delta.Imaging", newWsqIssue);
            //}
            //catch (Exception ex)
            //{
            //    LogLn("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            //    LogLn(ex.ToString());
            //    LogLn("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            //}
            //LogLn("-------- DONE --------");

            //var wsqIssueComment = (await client.Issue.Comment.GetForIssue("odalet", "Delta", wsqIssue.Number)).Single();
            //var newWsqIssue = (await client.Issue.GetForRepository("odalet", "Delta.Imaging", new RepositoryIssueRequest() { State = ItemState.Open })).Single();
            //var newWsqIssueComment = await client.Issue.Comment.Create("odalet", "Delta.Imaging",newWsqIssue.Number, 
            //    wsqIssueComment.Body);
            //LogLn("-------- DONE --------");

            /*
             
             bug, CapiNet, CertXplorer, duplicate, enhancement, nicetohave, WSQ
-----------------------------------------------------------
Issue #14 --> CertXplorer - Normalize the View model [Open]	enhancement, CertXplorer
Issue #13 --> CertXplorer - Loosing selected item in Certificates View [Open]	bug, CertXplorer
Issue #9 --> CapiNet - Support putty keys format [Open]	enhancement, CertXplorer, nicetohave, CapiNet
Issue #7 --> CertXplorer - UX: Certificates Tree [Open]	CertXplorer, nicetohave
Issue #6 --> CertXplorer - plugins UI [Open]	enhancement, CertXplorer
Issue #5 --> CertXplorer - Certificates Tools Plugin [Open]	enhancement, CertXplorer
Issue #4 --> CertXplorer - Options Dialog [Open]	enhancement, CertXplorer
Issue #2 --> CertXplorer - ToolStripRenderer [Open]	bug, CertXplorer
Issue #1 --> CertXplorer - MMC plugin should support Windows XP [Open]	bug, CertXplorer					
Issue #12 --> Missing copy/paste commands in exception box [Closed]	bug, CertXplorer, nicetohave
Issue #11 --> CertXplorer - Broken "Open Certificate" command [Closed]	bug, CertXplorer
Issue #10 --> CertXplorer - Broken "Open File" command [Closed]	bug, CertXplorer
Issue #8 --> CapiNet - Support Certificate Trust Lists [Closed]	enhancement, CertXplorer, CapiNet
Issue #3 --> CertXplorer - ToolStripRenderer [Closed]	bug, duplicate, CertXplorer

Issue #15 --> WSQ - Can't open a WSQ file with no resolution information [Closed]	bug, WSQ
              
             */


            var oldLabels = await client.Issue.Labels.GetForRepository("odalet", "Delta");
            var newLabels = await client.Issue.Labels.GetForRepository("odalet", "Delta.Cryptography");
            foreach (var label in oldLabels)
            {
                if (newLabels.Count(l => l.Name == label.Name) > 0)
                {
                    var lu = new LabelUpdate(label.Name, label.Color);
                    var nl = await client.Issue.Labels.Update("odalet", "Delta.Cryptography", label.Name, lu);
                    LogLn(string.Format("Label {0}'s color was updated to {1}", nl.Name, nl.Color));
                }
            }



            //LogLn("**********************************************************************");
            //LogLn("Migrating all Cryptography issues");
            //var isslist = issues.Where(i => i.Number != 15).OrderBy(i => i.Number);
            //foreach (var original in isslist)
            //{
            //    var n = original.Number;
            //    LogLn("-------------------------");
            //    LogLn("Processing Issue #" + n);
            //    try
            //    {
            //        // Labels
            //        //var newIssue = await client.Issue.Get("odalet", "Delta.Cryptography", n);
            //        var upd = new IssueUpdate();
            //        foreach (var ll in original.Labels.Select(l => l.Name))
            //            upd.Labels.Add(ll);
            //        upd.Labels.Add("migrated");

            //        await client.Issue.Update("odalet", "Delta.Cryptography", n, upd);
            //        LogLn("Updated labels for issue #" + n);

            //        //var lu = new LabelUpdate()

            //        //var title = original.Title;
            //        //if (n == 12) title = "CertXplorer - " + title;

            //        //var newIssue = new NewIssue(title);
            //        //newIssue.Body = original.Body + "\r\n\r\n_This issue was migrated from issue #" + n + " in former Delta repository_\r\n" + GetStatsText(original);
            //        //newIssue.Assignee = original.Assignee == null ? null : original.Assignee.Login;
            //        ////original.

            //        //// We have no milestones, let's create the issue
            //        //var migrated = await client.Issue.Create("odalet", "Delta.Cryptography", newIssue);
            //        //var nn = migrated.Number;
            //        //LogLn("Issue #" + nn + " successfully created from issue #" + n + "... Now migrating its comments");
            //        //if (original.Comments == 0) 
            //        //    LogLn("Done: No comments attached to this issue.");
            //        //else
            //        //{
            //        //    var comments = await client.Issue.Comment.GetForIssue("odalet", "Delta", n);
            //        //    foreach (var comment in comments)
            //        //    {
            //        //        var body = comment.Body;
            //        //        body += "\r\n\r\n_This comment was migrated from former Delta repository_\r\n" + GetStatsText(comment);
            //        //        var newComment = await client.Issue.Comment.Create("odalet", "Delta.Cryptography", nn, body);
            //        //        LogLn(string.Format("\tCreated comment #{0} for issue #{1}", newComment.Id, nn));
            //        //    }
            //        //}

            //        //// Close the issue?
            //        //if (original.State == ItemState.Closed)
            //        //{
            //        //    var iu = new IssueUpdate() { State = ItemState.Closed };
            //        //    migrated = await client.Issue.Update("odalet", "Delta.Cryptography", nn, iu);
            //        //    if (migrated.State == ItemState.Closed)
            //        //        LogLn("Issue #" + nn + " was successfully closed.");
            //        //    else LogLn("Could not close Issue #" + nn);
            //        //}
            //    }
            //    catch (Exception ex)
            //    {
            //        LogLn("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            //        LogLn(ex.ToString());
            //        LogLn("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            //    }
            //}

            LogLn("-------- DONE --------");
        }

        private string GetStatsText(Issue issue)
        {
            var builder = new StringBuilder();

            var user = issue.User == null ? "nobody" : issue.User.Login;
            var assignee = issue.Assignee == null ? "nobody" : issue.Assignee.Login;
            builder.AppendFormat("\t_Originally created by @{0} and assigned to @{1}_\r\n", user, assignee);
            builder.AppendFormat("\t_Originally created at {0}_\r\n", issue.CreatedAt.ToString("yyyy-MM-dd'T'HH:mm:ssK"));
            if (issue.UpdatedAt.HasValue)
                builder.AppendFormat("\t_Originally updated at {0}_\r\n", issue.UpdatedAt.Value.ToString("yyyy-MM-dd'T'HH:mm:ssK"));
            if (issue.ClosedAt.HasValue)
                builder.AppendFormat("\t_Originally closed at {0}_\r\n", issue.ClosedAt.Value.ToString("yyyy-MM-dd'T'HH:mm:ssK"));
            if (issue.Url != null)
                builder.AppendFormat("\t_Original url: {0}_\r\n", issue.Url.ToString());

            return builder.ToString();
        }
        private string GetStatsText(IssueComment comment)
        {
            var builder = new StringBuilder();

            var user = comment.User == null ? "nobody" : comment.User.Login;
            builder.AppendFormat("\t_Originally created by @{0}_\r\n", user);
            builder.AppendFormat("\t_Originally created at {0}_\r\n", comment.CreatedAt.ToString("yyyy-MM-dd'T'HH:mm:ssK"));
            if (comment.UpdatedAt.HasValue)
                builder.AppendFormat("\t_Originally updated at {0}_\r\n", comment.UpdatedAt.Value.ToString("yyyy-MM-dd'T'HH:mm:ssK"));            
            if (comment.Url != null)
                builder.AppendFormat("\t_Original url: {0}_\r\n", comment.Url.ToString());

            return builder.ToString();
        }

        private async void DumpIssue(Issue issue)
        {
            LogLn(issue.Title);
            LogLn(issue.Body);

            LogLn("Comments: ");
            var comments = await client.Issue.Comment.GetForIssue("odalet", "Delta", issue.Number);
            foreach (var comment in comments)
                LogLn("\t" + comment.User.Login + "/" + comment.User.Name + " - " + comment.Body);
            //foreach (var c)
            LogLn("Comments: " + issue.Comments);
        }

        private void LogLn(string text)
        {
            Log(text + "\r\n");
        }

        private void Log(string text)
        {
            logbox.AppendText(text);
            logbox.Select(text.Length, 0);
        }
    }
}
