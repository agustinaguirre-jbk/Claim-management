using DeltaApi.Domain.Claims;
using DeltaApi.Domain.ValueObjects;

namespace DeltaApi.Infrastructure.Mappings;

public static class ClaimMapper
{
    public static Claim MapToClaim(dynamic row)
    {
        var policyInfo = new PolicyInfo(
            policyNumber: row.policy_number,
            deltaFileNumber: row.delta_file_number,
            clientFileNumber: row.client_file_number
        );

        var injuryInfo = row.alleged_injury != null || row.injury_description != null || 
                        row.attorney_representation || row.liability != null || 
                        row.workers_compensation || row.exposure != null
            ? new InjuryInfo(
                allegedInjury: row.alleged_injury,
                injuryDescription: row.injury_description,
                attorneyRepresentation: row.attorney_representation,
                liability: row.liability,
                workersCompensation: row.workers_compensation,
                exposure: row.exposure
            )
            : null;

          return new Claim(
              id: row.claim_id,
              caseId: row.case_id,
              claimTypeId: row.claim_type_id,
              claimantId: row.claimant_id,
              clientId: row.client_id,
              policyInfo: policyInfo,
              doctorId: row.doctor_id,
              stateOfLossId: row.state_of_loss_id,
              injuryInfo: injuryInfo,
              createdByUser: 1 // Default value since the field doesn't exist in DB
          );
    }
}
