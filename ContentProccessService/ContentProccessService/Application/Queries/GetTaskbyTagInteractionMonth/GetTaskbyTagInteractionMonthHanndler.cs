﻿using ContentProccessService.Entities;
using ContentProccessService.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ContentProccessService.Application.Queries.GetTaskbyTagInteractionMonth
{
    public class GetTaskbyTagInteractionMonthHanndler : IRequestHandler<GetTaskbyTagInteractionMonthRequest, List<StatisticReturnModel>>
    {
        private readonly ContentoDbContext _context;
        public GetTaskbyTagInteractionMonthHanndler(ContentoDbContext contentodbContext)
        {
            _context = contentodbContext;
        }
        public async Task<List<StatisticReturnModel>> Handle(GetTaskbyTagInteractionMonthRequest request, CancellationToken cancellationToken)
        {

            var lstTasks = await _context.Statistics.Include(x => x.IdTaskNavigation).ThenInclude(x => x.TasksTags)
                .Where(x => x.CreatedDate >= DateTime.UtcNow.AddMonths(-1) && x.CreatedDate < DateTime.UtcNow && x.IdTaskNavigation.TasksTags.Any(y => y.IdTag == request.Id)).ToListAsync();
            var lstTagInter = new List<StatisticsModel>();
            var lstTagInterReturn = new List<StatisticReturnModel>();
            foreach (var item in lstTasks)
            {
                var lstTag = _context.TasksTags.Include(x => x.IdTagNavigation).Where(x => x.IdTask == item.IdTask).Select(x => x.IdTagNavigation).ToList();
                foreach (var item2 in lstTag)
                {
                    if (lstTagInter.Any(x => x.IdTags == item2.Id && x.Date.DayOfYear == item.CreatedDate.GetValueOrDefault().DayOfYear))
                    {
                        lstTagInter.Where(x => x.IdTags == item2.Id).FirstOrDefault().TimeInTeraction += item.Views ?? 0;
                    }
                    else
                    {
                        var Alori = new StatisticsModel();
                        Alori.IdTags = item2.Id;
                        Alori.Date = item.CreatedDate ?? DateTime.UtcNow;
                        Alori.TimeInTeraction += item.Views ?? 0;
                        lstTagInter.Add(Alori);
                    }
                }

            }
            foreach (var item in lstTagInter)
            {
                var testst = item;
                var test = lstTagInterReturn.Where(x => x.Date.DayOfYear == item.Date.DayOfYear).FirstOrDefault();
                if (test != null)
                {
                    var test2 = test.TimeInteraction.ToArray();
                    switch (item.IdTags)
                    {
                        case 1:
                            {

                                test2[0] += item.TimeInTeraction;
                                test.TimeInteraction = test2.ToList();
                                break;
                            }
                        case 2:
                            {
                                test2[1] += item.TimeInTeraction;
                                test.TimeInteraction = test2.ToList();
                                break;
                            }
                        case 3:
                            {

                                test2[2] += item.TimeInTeraction;
                                test.TimeInteraction = test2.ToList();
                                break;
                            }
                        case 4:
                            {

                                test2[3] += item.TimeInTeraction;
                                test.TimeInteraction = test2.ToList();
                                break;
                            }
                        case 5:
                            {

                                test2[4] += item.TimeInTeraction;
                                test.TimeInteraction = test2.ToList();
                                break;
                            }
                        case 6:
                            {

                                test2[5] += item.TimeInTeraction;
                                test.TimeInteraction = test2.ToList();
                                break;
                            }
                        case 7:
                            {

                                test2[6] += item.TimeInTeraction;
                                test.TimeInteraction = test2.ToList();
                                break;
                            }
                        case 8:
                            {

                                test2[7] += item.TimeInTeraction;
                                test.TimeInteraction = test2.ToList();
                                break;
                            }
                        case 9:
                            {

                                test2[8] += item.TimeInTeraction;
                                test.TimeInteraction = test2.ToList();
                                break;
                            }
                        case 10:
                            {

                                test2[9] += item.TimeInTeraction;
                                test.TimeInteraction = test2.ToList();
                                break;
                            }

                        default:
                            {
                                break;
                            }
                    }

                }
                else
                {
                    var statiscs = new StatisticReturnModel();
                    statiscs.Date = item.Date;
                    int[] arr = new int[10];
                    switch (item.IdTags)
                    {
                        case 1:
                            {

                                arr[0] += item.TimeInTeraction;
                                break;
                            }
                        case 2:
                            {
                                arr[1] += item.TimeInTeraction;
                                break;
                            }
                        case 3:
                            {

                                arr[2] += item.TimeInTeraction;
                                break;
                            }
                        case 4:
                            {

                                arr[3] += item.TimeInTeraction;
                                break;
                            }
                        case 5:
                            {

                                arr[4] += item.TimeInTeraction;
                                break;
                            }
                        case 6:
                            {

                                arr[5] += item.TimeInTeraction;
                                break;
                            }
                        case 7:
                            {

                                arr[6] += item.TimeInTeraction;
                                break;
                            }
                        case 8:
                            {

                                arr[7] += item.TimeInTeraction;
                                break;
                            }
                        case 9:
                            {

                                arr[8] += item.TimeInTeraction;
                                break;
                            }
                        case 10:
                            {

                                arr[9] += item.TimeInTeraction;
                                break;
                            }

                        default:
                            {
                                break;
                            }
                    }
                    statiscs.TimeInteraction = arr.ToList();
                    lstTagInterReturn.Add(statiscs);

                }

            }

            return lstTagInterReturn.OrderBy(x => x.Date).ToList();
        }
    }
}
