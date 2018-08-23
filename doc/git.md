# Multi-repos management

* **meta**:
	* [https://medium.com/@patrickleet/mono-repo-or-multi-repo-why-choose-one-when-you-can-have-both-e9c77bd0c668](https://medium.com/@patrickleet/mono-repo-or-multi-repo-why-choose-one-when-you-can-have-both-e9c77bd0c668) 
	* [https://github.com/mateodelnorte/meta](https://github.com/mateodelnorte/meta)
	* [https://medium.com/@patrickleet/developing-a-plugin-for-meta-bd2e9c39882d](https://medium.com/@patrickleet/developing-a-plugin-for-meta-bd2e9c39882d)
* monolithic vs micro-repos:
	* [https://github.com/IcaliaLabs/guides/wiki/Monolithic-vs-Micro-Repos](https://github.com/IcaliaLabs/guides/wiki/Monolithic-vs-Micro-Repos)
	* [https://stackoverflow.com/questions/31003910/monolith-git-repo-vs-micro-repos](https://stackoverflow.com/questions/31003910/monolith-git-repo-vs-micro-repos)
	* [http://danluu.com/monorepo/](http://danluu.com/monorepo/)
* Tools
	* repobuild: [https://github.com/chrisvana/repobuild](https://github.com/chrisvana/repobuild)
	* Bazel: [https://bazel.build/](https://bazel.build/)
	* Fabric: [http://www.fabfile.org/](http://www.fabfile.org/)
	* **myrepos**: [https://myrepos.branchable.com/](https://myrepos.branchable.com/)
	* Google's repo: [https://source.android.com/setup/develop/](https://source.android.com/setup/develop/)

## Trigger builds + Include/Exclude path

* VSTS:
	* [https://visualstudio.uservoice.com/forums/330519-visual-studio-team-services/suggestions/5279254-allow-git-ci-builds-to-monitor-changes-only-in-a-s](https://visualstudio.uservoice.com/forums/330519-visual-studio-team-services/suggestions/5279254-allow-git-ci-builds-to-monitor-changes-only-in-a-s)
	* [https://mitchdenny.com/path-filters-in-vsts-build/](https://mitchdenny.com/path-filters-in-vsts-build/)
	* [https://stackoverflow.com/questions/38426697/trigger-ci-build-only-on-changes-to-subfolder-in-vsts-was-tfs-online-using-gi](https://stackoverflow.com/questions/38426697/trigger-ci-build-only-on-changes-to-subfolder-in-vsts-was-tfs-online-using-gi)
	* [https://docs.microsoft.com/en-us/vsts/pipelines/build/triggers?view=vsts&tabs=yaml](https://docs.microsoft.com/en-us/vsts/pipelines/build/triggers?view=vsts&tabs=yaml)
* Gitlab (impossible)
	* [https://gitlab.com/gitlab-org/gitlab-ce/issues/19813](https://gitlab.com/gitlab-org/gitlab-ce/issues/19813)
	* [https://gitlab.com/gitlab-org/gitlab-ce/issues/19232](https://gitlab.com/gitlab-org/gitlab-ce/issues/19232)
* Jenkins (google: *jenkins git trigger exclude path*):
	* [https://issues.jenkins-ci.org/browse/JENKINS-25048](https://issues.jenkins-ci.org/browse/JENKINS-25048)
	* [https://stackoverflow.com/questions/12211227/excluded-regions-in-jenkins-with-git](https://stackoverflow.com/questions/12211227/excluded-regions-in-jenkins-with-git)
	* [https://github.com/KostyaSha/github-integration-plugin/issues/117](https://github.com/KostyaSha/github-integration-plugin/issues/117)
	* [https://github.com/jenkinsci/gitlab-plugin/issues/654](https://github.com/jenkinsci/gitlab-plugin/issues/654)
	* [https://support.cloudbees.com/hc/en-us/articles/226568007-How-to-Trigger-Non-Multibranch-Jobs-from-BitBucket-Server-](https://support.cloudbees.com/hc/en-us/articles/226568007-How-to-Trigger-Non-Multibranch-Jobs-from-BitBucket-Server-)
	* **[https://stackoverflow.com/questions/44901004/how-to-only-build-one-directory-in-a-jenkins-multi-branch-pipeline-job](https://stackoverflow.com/questions/44901004/how-to-only-build-one-directory-in-a-jenkins-multi-branch-pipeline-job)**
	* Chaining... [https://stackoverflow.com/questions/49703118/how-do-i-chain-jenkins-pipelines-from-a-checked-out-git-repo](https://stackoverflow.com/questions/49703118/how-do-i-chain-jenkins-pipelines-from-a-checked-out-git-repo)