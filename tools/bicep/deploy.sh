while getopts lt:u:p:f:fn:sbuName:subId:l: o; do
    case "$o" in
    t) tenant=$OPTARG ;;
    u) azure_USERNAME=$OPTARG ;;
    f) FPATH=$OPTARG ;;
    fn) FileName=$OPTARG ;;
    sbuName) subscriptionName=$OPTARG ;;
    subId) subscriptionId=$OPTARG ;;
    l) location=$OPTARG ;;
    esac
done

cd ${FPATH}

if [ !-f "${FileName}.bicepparam"]; then
    echo "The parameter file does not exist. Please use the command: az bicep generate-params --file ${FileName}.bicep --output-format bicepparam, parameter to generate the file."
fi

az login --user ${azure_USERNAME} --tenant ${tenant}

if [ -n "$subscriptionName"]; then
    az account set --subscription ${sbuName}
else
    az account set --subscription ${subId}
fi

az deployment sub create --location ${location} --parameters ${FileName}.bicepparam
