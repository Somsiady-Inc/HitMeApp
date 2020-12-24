
#!/usr/bin/env bash
LC_ALL=C

local_branch="$(git rev-parse --abbrev-ref HEAD)"
valid_branch_regex="^(feature|bugfix|release)\/([a-zA-Z0-9]+(-[a-zA-Z0-9]+)*)$"
ignored_branch_names=("main" "develop")

message="Your branch name does not follow the established naming convention. \
Supported branch names are: main, develop, feature/feature-name123, bugfix/bugfix-name123, release/release-name123."

if [[ "${ignored_branch_names[@]}" =~ "${local_branch}" ]] || [[ $local_branch =~ $valid_branch_regex ]]
then
    exit 0
fi

echo "$message"
exit 1
